'use strict';

import { Ball, Brick, Paddle } from './domain';

// Constants
const ASPECT_RATIO: number = 4 / 3;
const GRID_ROWS: number = 6;
const GRID_COLUMNS: number = 10;
const BASE_BRICK_GAP: number = 10;
const BASE_BRICK_FONT_SIZE: number = 50;
const BASE_PAUSE_ICON_WIDTH: number = 200;
const BASE_PAUSE_ICON_HEIGHT: number = 250;
const BASE_PAUSE_BAR_BORDER_RADIUS: number = 10;
const INITIAL_LIVES: number = 3;
const INITIAL_SCORE: number = 10;

// Function to set the dimensions of the game container
function setContainerDimensions(): [number, number] {
    if (containerWidth !== undefined) {
        previousContainerWidth = containerWidth;
    }

    const screenWidth: number = window.innerWidth;
    const screenHeight: number = window.innerHeight;

    // Calculate width and height based on aspect ratio
    if (screenWidth / screenHeight > ASPECT_RATIO) {
        containerWidth = screenHeight * ASPECT_RATIO;
        containerHeight = screenHeight;
    } else {
        containerWidth = screenWidth;
        containerHeight = screenWidth / ASPECT_RATIO;
    }

    proportionalPixel = containerWidth / 1327;
    
    return [containerWidth, containerHeight];
}

let previousContainerWidth: number, proportionalPixel: number = 0, containerWidth: number, containerHeight: number;
[containerWidth, containerHeight] = setContainerDimensions();

// Elements
const startMenuElement: HTMLElement = createDOMElement('div', {
    id: 'start-menu', class: 'menu-overlay',
    style: `display: block; 
    padding: ${20 * proportionalPixel}px; 
    border-radius: ${10 * proportionalPixel}px`
});
const startButtonElement: HTMLElement = createDOMElement('button', {
    id: 'start-button',
    text: 'START',
    style: `font-size: ${20 * proportionalPixel}px; 
    padding: ${10 * proportionalPixel}px ${20 * proportionalPixel}px; 
    margin: 0 ${10 * proportionalPixel}px; 
    border-radius: ${5 * proportionalPixel}px`
});
const gameContainer: HTMLElement = createDOMElement('div', {
    id: 'game-container',
    style: `width: ${containerWidth}px; 
    height: ${containerHeight}px; 
    border-width: ${proportionalPixel}px`
});
const brickContainer: HTMLElement = createDOMElement('div', { id: 'brick-container' });
const paddleElement: HTMLElement = createDOMElement('div', { id: 'paddle' });
const ballElement: HTMLElement = createDOMElement('div', { class: 'ball', style: `border-width: ${proportionalPixel}px` });
const countersElement: HTMLElement = createDOMElement('div', {
    id: 'counters',
    style: `padding: ${10 * proportionalPixel}px; font-size: ${20 * proportionalPixel}px`
});
const livesElement: HTMLElement = createDOMElement('h2', { id: 'lives', text: `Lives: ${INITIAL_LIVES}` });
const scoreElement: HTMLElement = createDOMElement('h2', { id: 'score', text: `Score: ${INITIAL_SCORE}` });
const baseBrick: HTMLElement = createDOMElement('div', {
    class: 'brick',
    style: `font-size: ${BASE_BRICK_FONT_SIZE * proportionalPixel}px; 
    text-shadow: ${2 * proportionalPixel}px ${2 * proportionalPixel}px ${4 * proportionalPixel}px #000; 
    border-width: ${proportionalPixel}px`
});
const pauseIcon: HTMLElement = createPauseIcon();

const paddle: Paddle = new Paddle(containerWidth, containerHeight, paddleElement);
let balls: Ball[] = [new Ball(containerWidth, containerHeight, ballElement)];

let bricks: Brick[] = [];
let lives: number = INITIAL_LIVES;
let score: number = INITIAL_SCORE;
let timerId: NodeJS.Timeout;
let timerDelayId: NodeJS.Timeout;
let lastExecution: number;
let msToWait: number;
let brickWidth: number;
let brickHeight: number;
let paused: boolean = false;
let softPaused: boolean = true;
let leftPressed: boolean = false;
let rightPressed: boolean = false;

// Function to create a DOM element
function createDOMElement(tag: string, options?: { id?: string, class?: string, text?: string, style?: string }): HTMLElement {
    const element: HTMLElement = document.createElement(tag);
    if (options) {
        if (options.id) element.id = options.id;
        if (options.class) element.classList.add(options.class);
        if (options.text) element.textContent = options.text;
        if (options.style) element.style.cssText = options.style;
    }
    return element;
}

// Function to create the pause icon
function createPauseIcon(): HTMLElement {
    const pauseIcon: HTMLElement = createDOMElement('div', { id: 'pause-icon', style: 'display: none' });
    for (let i = 0; i < 2; i++) {
        const bar: HTMLElement = createDOMElement('div', {
            class: 'bar',
            style: `border-radius: ${BASE_PAUSE_BAR_BORDER_RADIUS * proportionalPixel}px`
        });
        pauseIcon.appendChild(bar);
    }
    return pauseIcon;
}

// Initialize DOM elements
(function () {
    const title: HTMLElement = createDOMElement('h1', {
        text: 'ATARI BREAKOUT',
        style: `font-size: ${40 * proportionalPixel}px; margin-bottom: ${20 * proportionalPixel}px`
    });
    startMenuElement.appendChild(title);

    const topResults: HTMLElement = createDOMElement('div', { class: 'top-results', style: `margin: ${30 * proportionalPixel}px 0` });
    startMenuElement.appendChild(topResults);

    const topResultsTitle: HTMLElement = createDOMElement('h2', {
        text: 'Top 10 Results',
        style: `font-size: ${30 * proportionalPixel}px; margin-bottom: ${10 * proportionalPixel}px`
    });
    topResults.appendChild(topResultsTitle);

    const results: HTMLElement = createDOMElement('ol', { style: `font-size: ${14 * proportionalPixel}px` });
    topResults.appendChild(results);

    getScores().forEach(score => {
        let li: HTMLElement = createDOMElement('li', {
            text: `${score[1]} - ${score[0]}`,
            style: `font-size: ${18 * proportionalPixel}px; margin: ${5 * proportionalPixel}px 0`
        });
        results.appendChild(li);
    });

    startMenuElement.appendChild(startButtonElement);

    const keyBindings: HTMLElement = createDOMElement('div', {
        class: 'keybindings',
        style: `margin-top: ${20 * proportionalPixel}px`
    });

    const keys: string[] = ['←', '→', 'Esc'];
    keys.forEach(key => {
        const p: HTMLElement = createDOMElement('p', { style: `margin: ${5 * proportionalPixel}px 0; font-size: ${16 * proportionalPixel}px` });
        const span: HTMLElement = createDOMElement('span', {
            class: 'code-block',
            text: key,
            style: `padding: ${2 * proportionalPixel}px ${5 * proportionalPixel}px; border-width: ${proportionalPixel}px; border-radius: ${3 * proportionalPixel}px`
        });
        p.appendChild(span);

        if (key === 'Esc') {
            p.innerHTML += ' to pause/resume the game';
        } else {
            p.innerHTML += ' to move paddle ';
            p.innerHTML += key === '←' ? 'left' : 'right';
        }

        keyBindings.appendChild(p);
    });

    startMenuElement.appendChild(keyBindings);

    countersElement.appendChild(livesElement);
    countersElement.appendChild(scoreElement);

    gameContainer.appendChild(pauseIcon);
    gameContainer.appendChild(brickContainer);
    gameContainer.appendChild(paddleElement);
    gameContainer.appendChild(ballElement);
    gameContainer.appendChild(countersElement);

    document.body.appendChild(startMenuElement);
    document.body.appendChild(gameContainer);
})();

function decrementScore(): void {
    score--;
    scoreElement.innerText = 'Score: ' + score;
    lastExecution = Date.now();
}

function startTimer(): void {
    lastExecution = lastExecution || Date.now();

    if (msToWait) {
        timerDelayId = setTimeout(() => {
            timerId = setInterval(decrementScore, 1000);
            decrementScore();
        }, msToWait);
    } else {
        timerId = setInterval(decrementScore, 1000);
    }
}

function stopTimer(): void {
    msToWait = 1000 - ((Date.now() - lastExecution) % 1000);
    clearTimeout(timerDelayId);
    clearInterval(timerId);
}

function getScores(): [number, string][] {
    return JSON.parse(localStorage.getItem("scores") || "[]") || [];
}

function setScores(scores: [number, string][]): void {
    scores = scores.sort((a, b) => b[0] - a[0]).slice(0, 10);
    localStorage.setItem("scores", JSON.stringify(scores));
}


let border: number = proportionalPixel;  // ~1 @ 1440p 100%  ||  ~0.7 @ 1080p 100%

let brickGap: number = BASE_BRICK_GAP * proportionalPixel;
let remainingSpace: number = containerWidth / 100;
brickWidth = (containerWidth - remainingSpace * 2 - brickGap * (GRID_COLUMNS - 1)) / GRID_COLUMNS;
brickHeight = containerHeight / 3 / GRID_ROWS;

// Create bricks
(function () {
    for (let i = 0; i < GRID_ROWS; i++) {
        for (let j = 0; j < GRID_COLUMNS; j++) {
            let brickElement: HTMLElement = baseBrick.cloneNode(true) as HTMLElement;
            brickContainer.appendChild(brickElement);

            let brickType: string = 'normal';

            bricks.push(new Brick(
                remainingSpace + j * (brickWidth + brickGap),
                60 * proportionalPixel + i * (brickHeight + brickGap),
                brickWidth,
                brickHeight,
                brickElement,
                brickType));
        }
    }

    for (const multiplier of [2, 5, 10]) {
        for (let number = 0; number < 10 / multiplier; number++) {
            let i: number = -1;
            let j: number = -1;

            while (i === -1 || bricks[i * GRID_COLUMNS + j].type !== 'normal') {
                i = Math.floor(Math.random() * GRID_ROWS);
                j = Math.floor(Math.random() * GRID_COLUMNS);
            }

            const brElement: HTMLElement = brickContainer.children[i * GRID_COLUMNS + j] as HTMLElement;
            brElement.style.backgroundColor = 'darkorange';
            brElement.innerText = 'x' + multiplier;
            bricks[i * GRID_COLUMNS + j].type = 'x' + multiplier;
        }
    }
})();

window.addEventListener('resize', setContainerDimensions);
window.addEventListener('resize', updateItemSizes);

function updateItemSizes(): void {
    gameContainer.style.width = containerWidth + 'px';
    gameContainer.style.height = containerHeight + 'px';

    pauseIcon.style.width = BASE_PAUSE_ICON_WIDTH * proportionalPixel + 'px';
    pauseIcon.style.height = BASE_PAUSE_ICON_HEIGHT * proportionalPixel + 'px';
    Array.from(pauseIcon.children).forEach(bar => {
        (bar as HTMLElement).style.borderRadius = BASE_PAUSE_BAR_BORDER_RADIUS * proportionalPixel + 'px';
    });

    const zoomMultiplier: number = containerWidth / previousContainerWidth;

    paddle.update(zoomMultiplier);
    balls.forEach(ball => ball.update(zoomMultiplier));
    bricks.forEach(brick => brick.update(zoomMultiplier));
    border *= zoomMultiplier;

    gameContainer.style.borderWidth = border + 'px';
    countersElement.style.fontSize = parseFloat(countersElement.style.fontSize) * zoomMultiplier + 'px';

    document.querySelectorAll("#start-menu, #start-menu *").forEach(child => {
        (child as HTMLElement).style.borderWidth = border + 'px';

        ['fontSize', 'borderRadius', 'paddingTop', 'paddingBottom', 'paddingLeft', 'paddingRight', 'marginTop', 'marginBottom', 'marginLeft', 'marginRight'].forEach(property => {
            if ((child as HTMLElement).style[property as any]) {
                (child as HTMLElement).style[property as any] = parseFloat((child as HTMLElement).style[property as any]) * zoomMultiplier + 'px';
            }
        });
    });

    countersElement.style.padding = parseFloat(countersElement.style.padding) * zoomMultiplier + 'px';
}

document.addEventListener('keydown', function (event: KeyboardEvent) {
    if (['ArrowLeft', 'ArrowRight'].includes(event.key)) {
        if (event.key === 'ArrowRight') {
            rightPressed = true;
        } else {
            leftPressed = true;
        }

        if (softPaused && !paused && startMenuElement.style.display === 'none') {
            softPaused = false;
            startTimer();
        }
    } else if (event.key === 'Escape') {
        if (startMenuElement.style.display === 'none') {
            paused = !paused;

            if (paused) {
                pauseIcon.style.display = 'flex';
                stopTimer();
            } else {
                pauseIcon.style.display = 'none';
                startTimer();
            }
        }
    }
});

document.addEventListener('keyup', function (event: KeyboardEvent) {
    if (event.key === 'ArrowLeft') {
        leftPressed = false;
    } else if (event.key === 'ArrowRight') {
        rightPressed = false;
    }
});

async function restartGame(): Promise<void> {
    await new Promise(r => setTimeout(r, 100));
    let scores: [number, string][] = getScores();
    const playerName: string | null = prompt(`You scored ${score}. Enter your name: `);
    if (playerName) {
        scores.push([score, playerName]);
        setScores(scores);
    }

    window.location.reload();
}

function resetPositions(): void {
    paddle.x = (containerWidth - paddle.width) / 2;
    softPaused = true;
    stopTimer();
}

async function gameLoop(): Promise<void> {
    if (!paused && !softPaused) {
        for (const ball of balls) {
            let result: number = ball.move(containerWidth, containerHeight, paddle, bricks, scoreElement, score);

            if (result === -2) {
                await new Audio('sounds/win_err.wav').play();
                balls.splice(balls.indexOf(ball), 1);

                if (balls.length === 0) {
                    livesElement.innerText = 'Lives: ' + --lives;
                    balls.push(new Ball(containerWidth, containerHeight, ball.element));

                    if (lives === 0) {
                        await restartGame();
                        return;
                    } else {
                        resetPositions();
                    }
                } else {
                    ball.element.remove();
                }
                continue;
            } else if (result === -1) {
                await new Audio('sounds/hit.wav').play();
            } else if (result !== 0) {
                await new Audio('sounds/hit.wav').play();
                score += 10;
                scoreElement.innerText = 'Score: ' + score;

                if (result > 1) {
                    for (let i = 0; i < result - 1; i++) {
                        const newBall = ball.clone();

                        const offsetX = Math.random() * (ball.diameter * 2) - (ball.diameter);
                        const offsetY = Math.random() * (ball.diameter * 2) - (ball.diameter);

                        newBall.x += offsetX;
                        newBall.y += offsetY;

                        gameContainer.appendChild(newBall.element);
                        balls.push(newBall);
                    }
                }
            }

            if (bricks.length === 0) {
                for (let i = 0; i < balls.length - 1; i++) {
                    balls[i].element.remove();
                }
                balls = [new Ball(containerWidth, containerHeight, balls[balls.length - 1].element)];
                await restartGame();
                return;
            }

        }
        
        paddle.move(+rightPressed - +leftPressed, containerWidth);
    }

    requestAnimationFrame(gameLoop);
}

startButtonElement.addEventListener('click', async function (): Promise<void> {
    startMenuElement.style.display = 'none';
    await gameLoop();
});
