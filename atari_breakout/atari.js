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

let lives;
const livesElement = document.createElement('h1');

let score;
const scoreElement = document.createElement('h2');

let timerId;
let timerDelayId;
let lastExecution;
let msToWait;

let containerWidth, containerHeight, previousContainerWidth, previousContainerHeight;
let paused = false;
let softPaused = true;


function initializeDOM() {
    startMenuElement.classList.add('menu-overlay');
    startMenuElement.id = 'start-menu';

    const title = document.createElement('h1');
    title.textContent = 'ATARI BREAKOUT';
    startMenuElement.appendChild(title);

    const topResults = document.createElement('div');
    topResults.classList.add('top-results');
    const topResultsTitle = document.createElement('h2');
    topResultsTitle.textContent = 'Top 10 Results';
    const ol = document.createElement('ol');
    topResults.appendChild(topResultsTitle);
    topResults.appendChild(ol);
    startMenuElement.appendChild(topResults);
    
    startButtonElement.id = 'start-button';
    startButtonElement.textContent = 'START';
    startMenuElement.appendChild(startButtonElement);

    const keyBindings = document.createElement('div');
    keyBindings.classList.add('keybindings');
    const keys = ['←', '→', 'Esc'];
    keys.forEach(key => {
        const p = document.createElement('p');
        const span = document.createElement('span');
        span.classList.add('backtick');
        span.textContent = key;
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
    document.body.appendChild(startMenuElement);
    
    gameContainer.id = 'game-container';
    
    pauseIcon.id = 'pause-icon';
    pauseIcon.style.display = 'none';

    for (let i = 0; i < 2; i++) {
        const bar = document.createElement('div');
        bar.classList.add('bar');
        pauseIcon.appendChild(bar);
    }
    
    brickContainer.id = 'brick-container';
    paddleElement.id = 'paddle'
    ballElement.classList.add('ball');

    livesElement.id = 'lives';
    livesElement.textContent = 'Lives: 3';

    scoreElement.id = 'score';
    scoreElement.textContent = 'Score: 10';
    
    gameContainer.appendChild(pauseIcon);
    gameContainer.appendChild(brickContainer);
    gameContainer.appendChild(paddleElement);
    gameContainer.appendChild(ballElement);
    document.body.appendChild(gameContainer);
    document.body.appendChild(livesElement);
    document.body.appendChild(scoreElement);
}

function setupGame() {
    resetPositions();
    initializeBricks();

    score = 10;
    scoreElement.innerText = 'Score: ' + score;
    lives = 3;
    livesElement.innerText = 'Lives: ' + lives;
    startMenuElement.style.display = 'block';

    let results = document.querySelector('#start-menu > div > ol');
    // setScores([]);
    Array.from(results.children).forEach(li => li.remove());

    getScores().forEach(score => {
        let li = document.createElement('li');
        li.innerText = score[1] + ' - ' + score[0];
        results.appendChild(li);
    });
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


initializeDOM();

// Call the function initially and on window resize
setContainerDimensions();

let border = containerWidth / 1326;  // ~1 @ 1440p 100%  ||  ~0.7 @ 1080p 100%

const paddle = new Paddle(containerWidth, containerHeight, paddleElement);

const ball = new Ball(containerWidth, containerHeight, ballElement);
let balls = [ball];

brickGap = containerWidth / 133;  // ~10 @ 1440p 100%  ||  ~7.1 @ 1080p 100%
let remainingSpace = containerWidth / 100;
brickWidth = (containerWidth - remainingSpace * 2 - brickGap * (gridColumns - 1)) / gridColumns;  // ~40 @ 1440p 100%  ||  ~28.5 @ 1080p 100%
brickHeight = containerWidth / 4 / gridRows;  // ~20 @ 1440p 100%  ||  ~14.3 @ 1080p 100%
const baseBrick = document.createElement('div');
baseBrick.classList.add('brick');
baseBrick.style.borderWidth = border + 'px';

function initializeBricks() {
    if (bricks.size > 0) {
        bricks.forEach(brick => brick.element.remove());
        bricks.clear();
    }

    for (let i = 0; i < gridRows; i++) {
        for (let j = 0; j < gridColumns; j++) {
            let brickElement = baseBrick.cloneNode();
            brickContainer.appendChild(brickElement);

            let brickType = 'normal';

            bricks.push(new Brick(
                remainingSpace + j * (brickWidth + brickGap),
                border * 60 + i * (brickHeight + brickGap),
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

// Function to set the dimensions of the game container
function setContainerDimensions() {
    // paused = false;
    // pauseIcon.style.display = 'none';
    if (containerWidth !== undefined) {
        previousContainerWidth = containerWidth;
        previousContainerHeight = containerHeight;
    }

    const container = document.getElementById('game-container');
    const aspectRatio = 4 / 3; // Change this to match your desired aspect ratio
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

    container.style.width = containerWidth + 'px';
    container.style.height = containerHeight + 'px';
}


function updateItemSizes() {
    pauseIcon.style.width = containerWidth / 6.63 + 'px';
    pauseIcon.style.height = containerWidth / 5.304 + 'px';
    Array.from(pauseIcon.children).forEach(bar => {
        bar.style.borderRadius = containerWidth / 133 + 'px';
    });

    const zoomMultiplier = containerWidth / previousContainerWidth;

    paddle.update(zoomMultiplier);
    balls.forEach(ball => ball.update(zoomMultiplier));
    bricks.forEach(brick => brick.update(zoomMultiplier));
    border *= zoomMultiplier;

    // 942.6666666666666 707
    // 1326.6666666666665 995


    // trajectory.style.strokeWidth = border * 2 + 'px';
    gameContainer.style.borderWidth = border + 'px';

    livesElement.style.fontSize = parseFloat(getComputedStyle(livesElement).getPropertyValue('font-size')) * zoomMultiplier + 'px';
    scoreElement.style.fontSize = parseFloat(getComputedStyle(scoreElement).getPropertyValue('font-size')) * zoomMultiplier + 'px';
    
    startMenuElement.style.fontSize = parseFloat(getComputedStyle(startMenuElement).getPropertyValue('font-size')) * zoomMultiplier + 'px';
    document.querySelectorAll("#start-menu, #start-menu *").forEach(child => {
        const computedStyle = getComputedStyle(child);
        
        child.style.fontSize = parseFloat(computedStyle.getPropertyValue('font-size')) * zoomMultiplier + 'px';
        child.style.borderWidth = border + 'px';
        child.style.borderRadius = parseFloat(computedStyle.getPropertyValue('border-radius')) * zoomMultiplier + 'px';
        
        child.style.paddingTop = parseFloat(computedStyle.getPropertyValue('padding-top')) * zoomMultiplier + 'px';
        child.style.paddingBottom = parseFloat(computedStyle.getPropertyValue('padding-bottom')) * zoomMultiplier + 'px';
        child.style.paddingLeft = parseFloat(computedStyle.getPropertyValue('padding-left')) * zoomMultiplier + 'px';
        child.style.paddingRight = parseFloat(computedStyle.getPropertyValue('padding-right')) * zoomMultiplier + 'px';
        
        child.style.marginTop = parseFloat(computedStyle.getPropertyValue('margin-top')) * zoomMultiplier + 'px';
        child.style.marginBottom = parseFloat(computedStyle.getPropertyValue('margin-bottom')) * zoomMultiplier + 'px';
        child.style.marginLeft = parseFloat(computedStyle.getPropertyValue('margin-left')) * zoomMultiplier + 'px';
        child.style.marginRight = parseFloat(computedStyle.getPropertyValue('margin-right')) * zoomMultiplier + 'px';
    });
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

// function movePaddle() {
//     if (leftPressed && paddleX > 0) {
//         paddleX -= paddleSpeed;
//     }
//     if (rightPressed && paddleX < containerWidth - paddleWidth) {
//         paddleX += paddleSpeed;
//     }
//     paddleElement.style.left = paddleX + 'px';
// }


const trajectoryLine = document.getElementById('trajectory-line');
const svgContainer = document.getElementById('svg-container');

function updateTrajectoryLine() {
    const ballCenterX = ball.x + ball.diameter / 2;
    const ballCenterY = ball.y + ball.diameter / 2;
    const slope = ball.dy / (ball.dx + 0.00001); // Calculate slope of the ball's movement

    // Calculate the end position of the trajectory line
    let endX, endY;
    if (ball.dx >= 0) {
        endX = containerWidth;
        endY = ballCenterY + (containerWidth - ballCenterX) * slope;
    } else {
        endX = 0;
        endY = ballCenterY - ballCenterX * slope;
    }

    // Update SVG line attributes
    trajectoryLine.setAttribute('x1', ballCenterX);
    trajectoryLine.setAttribute('y1', ballCenterY);
    trajectoryLine.setAttribute('x2', endX);
    trajectoryLine.setAttribute('y2', endY);
}

// function setSvgDimensions() {
//     svgContainer.setAttribute('width', containerWidth);
//     svgContainer.setAttribute('height', containerHeight);
// }

// // Call setSvgDimensions initially and on window resize
// setSvgDimensions();
// window.addEventListener('resize', setSvgDimensions);


async function restartGame() {
    await new Promise(r => setTimeout(r, 100));
    let scores = getScores();
    const playerName = prompt(`You scored ${score}. Enter your name: `);
    if (playerName) {
        scores.push([score, playerName]);
        setScores(scores);
    }
    setupGame();
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
                    console.log(lives);

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
                        balls.push(ball.clone());
                    }
                }
            }

            if (bricks.length === 0) {
                // await new Audio('sounds/win.wav').play();
                for (let i = 0; i < balls.length - 1; i++) {
                    balls[i].element.remove();
                }
                balls = [new Ball(containerWidth, containerHeight, balls[balls.length - 1].element)];
                await restartGame();
                return;
            }

        }

        paddle.move(rightPressed - leftPressed, containerWidth);
        // updateTrajectoryLine(); // Call the function to update trajectory line
    }

    requestAnimationFrame(gameLoop);
}

setupGame();

startButtonElement.addEventListener('click', async function () {
    startMenuElement.style.display = 'none';
    await gameLoop();
});

