'use strict';


export class Ball {
    constructor(containerWidth, containerHeight, element) {
        this.element = element;
        this.speed = containerWidth / 442 * 1.4;  // ~3 @ 1440p 100%  ||  ~2.13 @ 1080p 100%
        this.diameter = containerWidth / 66;  // ~20 @ 1440p 100%  ||  ~14.3 @ 1080p 100%
        this.x = (containerWidth - this.diameter) / 2;
        this.y = containerHeight * 0.8;
        this.dx = 0;
        this.dy = this.speed;
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

    get diameter() {
        return this._diameter;
    }

    set diameter(diameter) {
        this._diameter = diameter;
        this.element.style.width = this.element.style.height = diameter + 'px';
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


export class Paddle {
    constructor(containerWidth, containerHeight, element) {
        this.element = element;
        this.speed = containerWidth / 133;  // ~10 @ 1440p 100%  ||  ~7.1 @ 1080p 100%
        this.width = containerWidth / 8;
        this.height = containerWidth / 133;
        this.x = (containerWidth - this.width) / 2;
        this.y = containerHeight - this.height * 2;
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


export class Brick {
    constructor(x, y, width, height, element, type = 'normal') {
        this.element = element;
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        this.type = type;
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
