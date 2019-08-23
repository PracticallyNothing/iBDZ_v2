class Vector2 {
    x: number;
    y: number;

    constructor(x: number = 0, y: number = 0) {
        this.x = x;
        this.y = y;
    }
}

function minus(v: Vector2) { return new Vector2(-v.x, -v.y); }
function add(v1: Vector2, v2: Vector2) { return new Vector2(v1.x + v2.x, v1.y + v2.y); }
function sub(v1: Vector2, v2: Vector2) { return new Vector2(v1.x - v2.x, v1.y - v2.y); }
function mult(v: Vector2, f: number) { return new Vector2(v.x * f, v.y * f); }
function div(v: Vector2, f: number) { return new Vector2(v.x / f, v.y / f); }
function len2(v: Vector2) { return v.x * v.x + v.y * v.y; }
function len(v: Vector2) { return Math.sqrt(len2(v)); }
function norm(v: Vector2) { return new Vector2(v.x / len(v), v.y / len(v)); }

class Camera {
    /** Position of the camera. */
    pos: Vector2;
    /** Camera's extra offset relative to its position. Used for dragging. */
    offset: Vector2;
    /** Camera zoom level. */
    zoom: number;

    constructor() {
        this.pos = new Vector2();
        this.offset = new Vector2();
        this.zoom = 1;
    }
}

var canvas: HTMLCanvasElement = <HTMLCanvasElement>document.getElementById("map")
var ctx: CanvasRenderingContext2D = canvas.getContext("2d")

canvas.width = window.innerWidth
canvas.height = 1000

var points: Vector2[] = [];
var camera: Camera = new Camera();

function getMousePosInCanvas(evt: MouseEvent) {
    var rect = canvas.getBoundingClientRect();
    return new Vector2(
        evt.clientX - rect.left,
        evt.clientY - rect.top
    );
}

let pointOuterRadius = 6,
    pointInnerRadius = 5

// The point that is currently moused over.
let hotPoint = null

function renderCanvas() {
    clear();

    for (let p of points)
        renderPoint(p, hotPoint == p)
}

let originalDownPos: Vector2 = new Vector2();
let lastMousePos: Vector2 = new Vector2();
let mousedown: boolean = false;

let dragStart: Vector2 = new Vector2();

canvas.onmousedown = function (evt) {
    dragStart = getMousePosInCanvas(evt);
    mousedown = true;
}
canvas.onmouseup = function (evt) {
    mousedown = false;
    camera.pos = add(camera.pos, camera.offset)
    camera.offset = new Vector2();
}

canvas.onmouseenter = function (evt) { document.body.style.cursor = "all-scroll"; }
canvas.onmouseleave = function (evt) { mousedown = false; document.body.style.cursor = "auto"; }

canvas.onmousemove = function (evt) {
    hotPoint = null;
    let mousePos: Vector2 = getMousePosInCanvas(evt);
    for (let p of points) {
        p = add(p, add(camera.pos, camera.offset))
        let mousedOver: boolean = len(sub(p, mousePos)) <= pointOuterRadius

        if (!mousedOver && hotPoint == p)
            hotPoint = null;
        else if (mousedOver && (hotPoint == null || hotPoint == p))
            hotPoint = p
    }

    if (hotPoint != null) {
        document.body.style.cursor = "pointer";
    } else {
        document.body.style.cursor = "all-scroll";
    }

    if (mousedown) {
        camera.offset = sub(mousePos, dragStart)
    }
}

function renderPoint(p: Vector2, active: boolean = false) {
    if (!active) {
        ctx.fillStyle = "#6666CC"
        ctx.strokeStyle = "#8888AA"
    } else {
        ctx.fillStyle = "#EF9A1A"
        ctx.strokeStyle = "#EFAA43"
    }

    let p2: Vector2 = add(p, add(camera.pos, camera.offset));

    ctx.beginPath()
    ctx.ellipse(
        p2.x, p2.y, // x, y
        pointInnerRadius, pointInnerRadius,   // radX, radY
        0,        // Rotation amt. (in radians)
        0, Math.PI * 2);    // startAngle, endAngle (both are in radians)
    ctx.fill()

    ctx.beginPath()
    ctx.ellipse(
        p2.x, p2.y, // x, y
        pointOuterRadius, pointOuterRadius,   // radX, radY
        0,        // Rotation amt. (in radians)
        0, Math.PI * 2);    // startAngle, endAngle (both are in radians)
    ctx.stroke()
}

function clear() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
}

points.push(new Vector2(15, 15));
points.push(new Vector2(30.5, 30.5));
points.push(new Vector2(67.5, 30.5));

//let dt: number = 0;
//let lastRenderTime: number = Date.now();
setInterval(() => {
    //    dt = (Date.now() - lastRenderTime) / 1000.0;
    renderCanvas();
    //    lastRenderTime = Date.now();
}, 16);