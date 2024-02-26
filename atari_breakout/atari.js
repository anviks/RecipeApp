'use strict';

import {Ball, Brick, Paddle} from './domain.js';

let bricks = [];

let gridRows = 6;
let gridColumns = 10;
let brickWidth;
let brickHeight;
let brickGap;

const startMenuElement = document.createElement('div');
const startButtonElement = document.createElement('button');
const pauseIcon = document.createElement('div');
const gameContainer = document.createElement('div');
const brickContainer = document.createElement('div');
const paddleElement = document.createElement('div');
const ballElement = document.createElement('div');
const countersElement = document.createElement('div');
const baseBrick = document.createElement('div');

let lives;
const livesElement = document.createElement('h2');

let score;
const scoreElement = document.createElement('h2');

let timerId;
let timerDelayId;
let lastExecution;
let msToWait;

let containerWidth, containerHeight, previousContainerWidth, previousContainerHeight;
let proportionalPixel;
let paused = false;
let softPaused = true;


// Function to set the dimensions of the game container
function setContainerDimensions() {
    if (containerWidth !== undefined) {
        previousContainerWidth = containerWidth;
        previousContainerHeight = containerHeight;
    }

    const aspectRatio = 4 / 3;
    const screenWidth = window.innerWidth;
    const screenHeight = window.innerHeight;

    // Calculate width and height based on aspect ratio
    if (screenWidth / screenHeight > aspectRatio) {
        containerWidth = screenHeight * aspectRatio;
        containerHeight = screenHeight;
    } else {
        containerWidth = screenWidth;
        containerHeight = screenWidth / aspectRatio;
    }

    proportionalPixel = containerWidth / 1327;
}


function initializeDOM() {
    gameContainer.id = 'game-container';
    gameContainer.style.width = containerWidth + 'px';
    gameContainer.style.height = containerHeight + 'px';
    gameContainer.style.borderWidth = proportionalPixel + 'px';

    startMenuElement.classList.add('menu-overlay');
    startMenuElement.id = 'start-menu';
    startMenuElement.style.display = 'block';
    startMenuElement.style.padding = 20 * proportionalPixel + 'px';
    startMenuElement.style.borderRadius = 10 * proportionalPixel + 'px';

    const title = document.createElement('h1');
    title.textContent = 'ATARI BREAKOUT';
    title.style.fontSize = 40 * proportionalPixel + 'px';
    title.style.marginBottom = 20 * proportionalPixel + 'px';
    startMenuElement.appendChild(title);

    const topResults = document.createElement('div');
    topResults.classList.add('top-results');
    topResults.style.marginTop = topResults.style.marginBottom = 30 * proportionalPixel + 'px';
    startMenuElement.appendChild(topResults);

    const topResultsTitle = document.createElement('h2');
    topResultsTitle.textContent = 'Top 10 Results';
    topResultsTitle.style.fontSize = 30 * proportionalPixel + 'px';
    topResultsTitle.style.marginBottom = 10 * proportionalPixel + 'px';
    topResults.appendChild(topResultsTitle);

    const results = document.createElement('ol');
    results.style.fontSize = 14 * proportionalPixel + 'px';
    topResults.appendChild(results);

    getScores().forEach(score => {
        let li = document.createElement('li');
        li.style.fontSize = 18 * proportionalPixel + 'px';
        li.style.marginBottom = li.style.marginTop = 5 * proportionalPixel + 'px';
        li.innerText = score[1] + ' - ' + score[0];
        results.appendChild(li);
    });

    startButtonElement.id = 'start-button';
    startButtonElement.textContent = 'START';
    startButtonElement.style.fontSize = 20 * proportionalPixel + 'px';
    startButtonElement.style.paddingTop = startButtonElement.style.paddingBottom = 10 * proportionalPixel + 'px';
    startButtonElement.style.paddingLeft = startButtonElement.style.paddingRight = 20 * proportionalPixel + 'px';
    startButtonElement.style.marginLeft = startButtonElement.style.marginRight = 10 * proportionalPixel + 'px';
    startButtonElement.style.borderRadius = 5 * proportionalPixel + 'px';
    startMenuElement.appendChild(startButtonElement);

    const keyBindings = document.createElement('div');
    keyBindings.classList.add('keybindings');
    keyBindings.style.marginTop = 20 * proportionalPixel + 'px';
    const keys = ['←', '→', 'Esc'];
    keys.forEach(key => {
        const p = document.createElement('p');
        const span = document.createElement('span');
        span.style.paddingTop = span.style.paddingBottom = 2 * proportionalPixel + 'px';
        span.style.paddingLeft = span.style.paddingRight = 5 * proportionalPixel + 'px';
        span.style.borderWidth = proportionalPixel + 'px';
        span.style.borderRadius = 3 * proportionalPixel + 'px';

        span.classList.add('code-block');
        span.textContent = key;
        p.appendChild(span);
        if (key === 'Esc') {
            p.innerHTML += ' to pause/resume the game';
        } else {
            p.innerHTML += ' to move paddle ';
            p.innerHTML += key === '←' ? 'left' : 'right';
        }

        p.style.marginTop = p.style.marginBottom = 5 * proportionalPixel + 'px';
        p.style.fontSize = 16 * proportionalPixel + 'px';

        keyBindings.appendChild(p);
    });

    startMenuElement.appendChild(keyBindings);
    document.body.appendChild(startMenuElement);

    pauseIcon.id = 'pause-icon';
    pauseIcon.style.display = 'none';

    for (let i = 0; i < 2; i++) {
        const bar = document.createElement('div');
        bar.classList.add('bar');
        bar.style.borderRadius = 10 * proportionalPixel + 'px';
        pauseIcon.appendChild(bar);
    }

    baseBrick.classList.add('brick');
    baseBrick.style.borderWidth = proportionalPixel + 'px';
    baseBrick.style.fontSize = 50 * proportionalPixel + 'px';
    baseBrick.style.textShadow = `${2 * proportionalPixel}px ${2 * proportionalPixel}px ${4 * proportionalPixel}px #000`;

    brickContainer.id = 'brick-container';
    paddleElement.id = 'paddle'
    ballElement.classList.add('ball');
    ballElement.style.borderWidth = proportionalPixel + 'px';

    lives = 3;
    livesElement.id = 'lives';
    livesElement.textContent = 'Lives: ' + lives;

    score = 10;
    scoreElement.id = 'score';
    scoreElement.textContent = 'Score: ' + score;

    countersElement.id = 'counters';
    countersElement.style.padding = 10 * proportionalPixel + 'px';
    countersElement.style.fontSize = 20 * proportionalPixel + 'px';
    countersElement.appendChild(livesElement);
    countersElement.appendChild(scoreElement);

    gameContainer.appendChild(pauseIcon);
    gameContainer.appendChild(brickContainer);
    gameContainer.appendChild(paddleElement);
    gameContainer.appendChild(ballElement);
    document.body.appendChild(gameContainer);
    gameContainer.appendChild(countersElement);
}

function decrementScore() {
    score--;
    scoreElement.innerText = 'Score: ' + score;
    lastExecution = Date.now();
}

function startTimer() {
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

function stopTimer() {
    msToWait = 1000 - ((Date.now() - lastExecution) % 1000);
    clearTimeout(timerDelayId);
    clearInterval(timerId);
}

function getScores() {
    return JSON.parse(localStorage.getItem("scores")) || [];
}

// Function to set scores in local storage
function setScores(scores) {
    scores = scores.sort((a, b) => b[0] - a[0]).slice(0, 10);
    localStorage.setItem("scores", JSON.stringify(scores));
}


setContainerDimensions();
initializeDOM();

let border = proportionalPixel;  // ~1 @ 1440p 100%  ||  ~0.7 @ 1080p 100%

const paddle = new Paddle(containerWidth, containerHeight, paddleElement);

const ball = new Ball(containerWidth, containerHeight, ballElement);
let balls = [ball];

brickGap = 10 * proportionalPixel;  // ~10 @ 1440p 100%  ||  ~7.1 @ 1080p 100%
let remainingSpace = containerWidth / 100;
brickWidth = (containerWidth - remainingSpace * 2 - brickGap * (gridColumns - 1)) / gridColumns;  // ~40 @ 1440p 100%  ||  ~28.5 @ 1080p 100%
brickHeight = containerWidth / 4 / gridRows;  // ~20 @ 1440p 100%  ||  ~14.3 @ 1080p 100%

function initializeBricks() {
    for (let i = 0; i < gridRows; i++) {
        for (let j = 0; j < gridColumns; j++) {
            let brickElement = baseBrick.cloneNode();
            brickContainer.appendChild(brickElement);

            let brickType = 'normal';

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
            let i;
            let j;

            while (i === undefined || bricks[i * gridColumns + j].type !== 'normal') {
                i = Math.floor(Math.random() * gridRows);
                j = Math.floor(Math.random() * gridColumns);
            }

            const brElement = brickContainer.children[i * gridColumns + j];
            brElement.style.backgroundColor = 'darkorange';
            brElement.innerText = 'x' + multiplier;
            bricks[i * gridColumns + j].type = 'x' + multiplier;
        }
    }
}

window.addEventListener('resize', setContainerDimensions);
window.addEventListener('resize', updateItemSizes);


function updateItemSizes() {
    gameContainer.style.width = containerWidth + 'px';
    gameContainer.style.height = containerHeight + 'px';

    pauseIcon.style.width = 200 * proportionalPixel + 'px';
    pauseIcon.style.height = 250 * proportionalPixel + 'px';
    Array.from(pauseIcon.children).forEach(bar => {
        bar.style.borderRadius = 10 * proportionalPixel + 'px';
    });

    const zoomMultiplier = containerWidth / previousContainerWidth;

    paddle.update(zoomMultiplier);
    balls.forEach(ball => ball.update(zoomMultiplier));
    bricks.forEach(brick => brick.update(zoomMultiplier));
    border *= zoomMultiplier;

    gameContainer.style.borderWidth = border + 'px';
    countersElement.style.fontSize = parseFloat(countersElement.style.fontSize) * zoomMultiplier + 'px';

    document.querySelectorAll("#start-menu, #start-menu *").forEach(child => {
        child.style.fontSize = parseFloat(child.style.fontSize) * zoomMultiplier + 'px';
        child.style.borderWidth = border + 'px';
        child.style.borderRadius = parseFloat(child.style.borderRadius) * zoomMultiplier + 'px';

        child.style.paddingTop = parseFloat(child.style.paddingTop) * zoomMultiplier + 'px';
        child.style.paddingBottom = parseFloat(child.style.paddingBottom) * zoomMultiplier + 'px';
        child.style.paddingLeft = parseFloat(child.style.paddingLeft) * zoomMultiplier + 'px';
        child.style.paddingRight = parseFloat(child.style.paddingRight) * zoomMultiplier + 'px';

        child.style.marginTop = parseFloat(child.style.marginTop) * zoomMultiplier + 'px';
        child.style.marginBottom = parseFloat(child.style.marginBottom) * zoomMultiplier + 'px';
        child.style.marginLeft = parseFloat(child.style.marginLeft) * zoomMultiplier + 'px';
        child.style.marginRight = parseFloat(child.style.marginRight) * zoomMultiplier + 'px';
    });

    countersElement.style.padding = parseFloat(countersElement.style.padding) * zoomMultiplier + 'px';
}


let leftPressed = false;
let rightPressed = false;

document.addEventListener('keydown', function (event) {
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

document.addEventListener('keyup', function (event) {
    if (event.key === 'ArrowLeft') {
        leftPressed = false;
    } else if (event.key === 'ArrowRight') {
        rightPressed = false;
    }
});

async function restartGame() {
    await new Promise(r => setTimeout(r, 100));
    let scores = getScores();
    const playerName = prompt(`You scored ${score}. Enter your name: `);
    if (playerName) {
        scores.push([score, playerName]);
        setScores(scores);
    }

    window.location.reload();
}

function resetPositions() {
    paddle.x = (containerWidth - paddle.width) / 2;
    softPaused = true;
    stopTimer();
}

async function gameLoop() {
    if (!paused && !softPaused) {
        for (const ball of balls) {
            let result = ball.move(containerWidth, containerHeight, paddle, bricks, scoreElement, score);

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

        paddle.move(rightPressed - leftPressed, containerWidth);
    }

    requestAnimationFrame(gameLoop);
}

initializeBricks();

startButtonElement.addEventListener('click', async function () {
    startMenuElement.style.display = 'none';
    await gameLoop();
});

