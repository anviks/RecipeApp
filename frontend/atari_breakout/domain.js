'use strict';

const BALL_SPEED_FACTOR = 315.7;
const BALL_DIAMETER_FACTOR = 66;

const PADDLE_SPEED_FACTOR = 133;
const PADDLE_WIDTH_FACTOR = 8;
const PADDLE_HEIGHT_FACTOR = 133;

class BaseGameElement {
    constructor(element) {
        this.element = element;
    }

    get x() {
        return this._x;
    }

    set x(x) {
        this._x = x;
        this.element.style.left = x + 'px';
    }

    get y() {
        return this._y;
    }

    set y(y) {
        this._y = y;
        this.element.style.top = y + 'px';
    }

    get width() {
        return this._width;
    }

    set width(width) {
        this._width = width;
        this.element.style.width = width + 'px';
    }

    get height() {
        return this._height;
    }

    set height(height) {
        this._height = height;
        this.element.style.height = height + 'px';
    }
}


export class Ball extends BaseGameElement {
    constructor(containerWidth, containerHeight, element) {
        super(element);
        this.speed = containerWidth / BALL_SPEED_FACTOR;  // ~3 @ 1440p 100%  ||  ~2.13 @ 1080p 100%
        this.diameter = containerWidth / BALL_DIAMETER_FACTOR;  // ~20 @ 1440p 100%  ||  ~14.3 @ 1080p 100%
        this.x = (containerWidth - this.diameter) / 2;
        this.y = containerHeight * 0.8;
        this.dx = 0;
        this.dy = this.speed;
    }

    get diameter() {
        return this.height;
    }

    set diameter(diameter) {
        this.height = this.width = diameter;
    }

    clone() {
        let newBall = new Ball(0, 0, this.element.cloneNode());
        this.element.parentElement.appendChild(newBall.element);

        newBall.speed = this.speed;
        newBall.diameter = this.diameter;
        newBall.x = this.x;
        newBall.y = this.y;
        newBall.dx = this.dx;
        newBall.dy = this.dy;

        return newBall;
    }

    update(zoomMultiplier) {
        ['speed', 'diameter', 'x', 'y', 'dx', 'dy'].forEach(property => {
            this[property] *= zoomMultiplier;
        });
        
        this.element.style.borderWidth = parseFloat(this.element.style.borderWidth) * zoomMultiplier + 'px';
    }

    move(containerWidth, containerHeight, paddle, bricks, scoreElement, score) {
        this.x += this.dx;
        this.y += this.dy;

        // Check for collision with walls
        if (this.x < 0 || this.x > containerWidth - this.diameter) {
            this.dx = -this.dx;
            return -1;
        }
        if (this.y < 0) {
            this.dy = -this.dy;
            return -1;
        }
        if (this.y > containerHeight - this.diameter) {
            this.dy = -this.dy;
            return -2;
        }

        // Check for collision with paddle
        if (
            this.y >= paddle.y - this.diameter &&
            this.x + this.diameter >= paddle.x &&
            this.x <= paddle.x + paddle.width
        ) {
            let angleMultiplier = (this.x - paddle.x - paddle.width / 2) / paddle.width * 1.5;
            this.dx = angleMultiplier * this.speed;
            this.dy = -Math.sqrt(this.speed ** 2 - this.dx ** 2);
            return -1;
        }

        // Check for collision with bricks
        for (const brick of bricks) {
            if (this.x <= brick.x + brick.width && this.x + this.diameter >= brick.x && this.y <= brick.y + brick.height && this.y + this.diameter >= brick.y) {
                scoreElement.innerText = 'Score: ' + score;

                brick.element.remove();
                bricks.splice(bricks.indexOf(brick), 1);

                // Determine collision side
                const overlapX = Math.abs(Math.min(this.x + this.diameter / 2, brick.x + brick.width) - Math.max(this.x + this.diameter / 2, brick.x));
                const overlapY = Math.abs(Math.min(this.y + this.diameter / 2, brick.y + brick.height) - Math.max(this.y + this.diameter / 2, brick.y));

                if (overlapX > overlapY) {
                    this.dx = -this.dx;
                } else {
                    this.dy = -this.dy;
                }

                if (brick.type === 'x2') {
                    return 2;
                } else if (brick.type === 'x5') {
                    return 5;
                } else if (brick.type === 'x10') {
                    return 10;
                }

                return 1;
            }
        }

        return 0;
    }
}

export class Paddle extends BaseGameElement {
    constructor(containerWidth, containerHeight, element) {
        super(element);
        this.speed = containerWidth / PADDLE_SPEED_FACTOR;  // ~10 @ 1440p 100%  ||  ~7.1 @ 1080p 100%
        this.width = containerWidth / PADDLE_WIDTH_FACTOR;
        this.height = containerWidth / PADDLE_HEIGHT_FACTOR;
        this.x = (containerWidth - this.width) / 2;
        this.y = containerHeight - this.height * 2;
    }

    update(zoomMultiplier) {
        ['speed', 'width', 'height', 'x', 'y'].forEach(property => {
            this[property] *= zoomMultiplier;
        });
    }

    move(direction, containerWidth) {
        if (direction === 0) {
            return;
        }

        this.x += this.speed * direction;

        if (this.x < 0) {
            this.x = 0;
        } else if (this.x > containerWidth - this.width) {
            this.x = containerWidth - this.width;
        }
    }
}


export class Brick extends BaseGameElement {
    constructor(x, y, width, height, element, type = 'normal') {
        super(element);
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        this.type = type;
    }

    update(zoomMultiplier) {
        ['x', 'y', 'width', 'height'].forEach(property => {
            this[property] *= zoomMultiplier;
        });

        const fontSize = this.element.style.fontSize
            || window.getComputedStyle(this.element).getPropertyValue('font-size');

        this.element.style.fontSize = parseFloat(fontSize) * zoomMultiplier + 'px';
        const newBorderWidth = parseFloat(this.element.style.borderWidth) * zoomMultiplier;
        this.element.style.borderWidth = newBorderWidth + 'px';

        this.element.style.textShadow = `${newBorderWidth * 2}px ${newBorderWidth * 2}px ${newBorderWidth * 4}px #000`;
    }
}
