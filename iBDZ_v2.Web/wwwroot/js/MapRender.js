var Vector2 = /** @class */ (function () {
    function Vector2(x, y) {
        if (x === void 0) { x = 0; }
        if (y === void 0) { y = 0; }
        this.x = x;
        this.y = y;
    }
    return Vector2;
}());
function minus(v) { return new Vector2(-v.x, -v.y); }
function add(v1, v2) { return new Vector2(v1.x + v2.x, v1.y + v2.y); }
function sub(v1, v2) { return new Vector2(v1.x - v2.x, v1.y - v2.y); }
function mult(v, f) { return new Vector2(v.x * f, v.y * f); }
function div(v, f) { return new Vector2(v.x / f, v.y / f); }
function len2(v) { return v.x * v.x + v.y * v.y; }
function len(v) { return Math.sqrt(len2(v)); }
function norm(v) { return new Vector2(v.x / len(v), v.y / len(v)); }
var Camera = /** @class */ (function () {
    function Camera() {
        this.pos = new Vector2();
        this.offset = new Vector2();
        this.zoom = 1;
    }
    return Camera;
}());
var canvas = document.getElementById("map");
var ctx = canvas.getContext("2d");
canvas.width = window.innerWidth;
canvas.height = 1000;
var points = [];
var camera = new Camera();
function getMousePosInCanvas(evt) {
    var rect = canvas.getBoundingClientRect();
    return new Vector2(evt.clientX - rect.left, evt.clientY - rect.top);
}
var pointOuterRadius = 6, pointInnerRadius = 5;
// The point that is currently moused over.
var hotPoint = null;
function renderCanvas() {
    clear();
    for (var _i = 0, points_1 = points; _i < points_1.length; _i++) {
        var p = points_1[_i];
        renderPoint(p, hotPoint == p);
    }
}
var originalDownPos = new Vector2();
var lastMousePos = new Vector2();
var mousedown = false;
var dragStart = new Vector2();
canvas.onmousedown = function (evt) {
    dragStart = getMousePosInCanvas(evt);
    mousedown = true;
};
canvas.onmouseup = function (evt) {
    mousedown = false;
    camera.pos = add(camera.pos, camera.offset);
    camera.offset = new Vector2();
};
canvas.onmouseenter = function (evt) { document.body.style.cursor = "all-scroll"; };
canvas.onmouseleave = function (evt) { mousedown = false; document.body.style.cursor = "auto"; };
canvas.onmousemove = function (evt) {
    hotPoint = null;
    var mousePos = getMousePosInCanvas(evt);
    for (var _i = 0, points_2 = points; _i < points_2.length; _i++) {
        var p = points_2[_i];
        p = add(p, add(camera.pos, camera.offset));
        var mousedOver = len(sub(p, mousePos)) <= pointOuterRadius;
        if (!mousedOver && hotPoint == p)
            hotPoint = null;
        else if (mousedOver && (hotPoint == null || hotPoint == p))
            hotPoint = p;
    }
    if (hotPoint != null) {
        document.body.style.cursor = "pointer";
    }
    else {
        document.body.style.cursor = "all-scroll";
    }
    if (mousedown) {
        camera.offset = sub(mousePos, dragStart);
    }
};
function renderPoint(p, active) {
    if (active === void 0) { active = false; }
    if (!active) {
        ctx.fillStyle = "#6666CC";
        ctx.strokeStyle = "#8888AA";
    }
    else {
        ctx.fillStyle = "#EF9A1A";
        ctx.strokeStyle = "#EFAA43";
    }
    var p2 = add(p, add(camera.pos, camera.offset));
    ctx.beginPath();
    ctx.ellipse(p2.x, p2.y, // x, y
    pointInnerRadius, pointInnerRadius, // radX, radY
    0, // Rotation amt. (in radians)
    0, Math.PI * 2); // startAngle, endAngle (both are in radians)
    ctx.fill();
    ctx.beginPath();
    ctx.ellipse(p2.x, p2.y, // x, y
    pointOuterRadius, pointOuterRadius, // radX, radY
    0, // Rotation amt. (in radians)
    0, Math.PI * 2); // startAngle, endAngle (both are in radians)
    ctx.stroke();
}
function clear() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
}
points.push(new Vector2(15, 15));
points.push(new Vector2(30.5, 30.5));
points.push(new Vector2(67.5, 30.5));
//let dt: number = 0;
//let lastRenderTime: number = Date.now();
setInterval(function () {
    //    dt = (Date.now() - lastRenderTime) / 1000.0;
    renderCanvas();
    //    lastRenderTime = Date.now();
}, 16);
//# sourceMappingURL=MapRender.js.map