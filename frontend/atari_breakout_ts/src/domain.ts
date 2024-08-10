'use strict';

const BALL_SPEED_FACTOR: number = 315.7;
const BALL_DIAMETER_FACTOR: number = 66;

const PADDLE_SPEED_FACTOR: number = 133;
const PADDLE_WIDTH_FACTOR: number = 8;
const PADDLE_HEIGHT_FACTOR: number = 133;

class BaseGameElement {
    element: HTMLElement;
    private _x!: number;
    private _y!: number;
    private _width!: number;
    private _height!: number;

    constructor(element: HTMLElement) {
        this.element = element;
    }

    get x(): number {
        return this._x;
    }

    set x(x: number) {
        this._x = x;
        this.element.style.left = x + 'px';
    }

    get y(): number {
        return this._y;
    }

    set y(y: number) {
        this._y = y;
        this.element.style.top = y + 'px';
    }

    get width(): number {
        return this._width;
    }

    set width(width: number) {
        this._width = width;
        this.element.style.width = width + 'px';
    }

    get height(): number {
        return this._height;
    }

    set height(height: number) {
        this._height = height;
        this.element.style.height = height + 'px';
    }
}

export class Ball extends BaseGameElement {
    speed: number;
    dx: number;
    dy: number;

    constructor(containerWidth: number, containerHeight: number, element: HTMLElement) {
        super(element);
        this.speed = containerWidth / BALL_SPEED_FACTOR;
        this.diameter = containerWidth / BALL_DIAMETER_FACTOR;
        this.x = (containerWidth - this.diameter) / 2;
        this.y = containerHeight * 0.8;
        this.dx = 0;
        this.dy = this.speed;
    }

    get diameter(): number {
        return this.height;
    }

    set diameter(diameter: number) {
        this.height = this.width = diameter;
    }

    clone(): Ball {
        let newBall = new Ball(0, 0, this.element.cloneNode() as HTMLElement);
        this.element.parentElement!.appendChild(newBall.element);

        newBall.speed = this.speed;
        newBall.diameter = this.diameter;
        newBall.x = this.x;
        newBall.y = this.y;
        newBall.dx = this.dx;
        newBall.dy = this.dy;

        return newBall;
    }

    update(zoomMultiplier: number): void {
        ['speed', 'diameter', 'x', 'y', 'dx', 'dy'].forEach(property => {
            (this as any)[property] *= zoomMultiplier;
        });

        this.element.style.borderWidth = parseFloat(this.element.style.borderWidth) * zoomMultiplier + 'px';
    }

    move(containerWidth: number, containerHeight: number, paddle: Paddle, bricks: Brick[], scoreElement: HTMLElement, score: number): number {
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
    speed: number;

    constructor(containerWidth: number, containerHeight: number, element: HTMLElement) {
        super(element);
        this.speed = containerWidth / PADDLE_SPEED_FACTOR;
        this.width = containerWidth / PADDLE_WIDTH_FACTOR;
        this.height = containerWidth / PADDLE_HEIGHT_FACTOR;
        this.x = (containerWidth - this.width) / 2;
        this.y = containerHeight - this.height * 2;
    }

    update(zoomMultiplier: number): void {
        ['speed', 'width', 'height', 'x', 'y'].forEach(property => {
            (this as any)[property] *= zoomMultiplier;
        });
    }

    move(direction: number, containerWidth: number): void {
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
    type: string;

    constructor(x: number, y: number, width: number, height: number, element: HTMLElement, type: string = 'normal') {
        super(element);
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        this.type = type;
    }

    update(zoomMultiplier: number): void {
        ['x', 'y', 'width', 'height'].forEach(property => {
            (this as any)[property] *= zoomMultiplier;
        });
        
        const fontSize = this.element.style.fontSize || window.getComputedStyle(this.element).getPropertyValue('font-size');
        this.element.style.fontSize = parseFloat(fontSize) * zoomMultiplier + 'px';
        const newBorderWidth = parseFloat(this.element.style.borderWidth) * zoomMultiplier;
        this.element.style.borderWidth = newBorderWidth + 'px';

        this.element.style.textShadow = `${newBorderWidth * 2}px ${newBorderWidth * 2}px ${newBorderWidth * 4}px #000`;
    }
}
