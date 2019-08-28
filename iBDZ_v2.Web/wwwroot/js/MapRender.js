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
        this.targetZoom = 1;
    }
    return Camera;
}());
var canvas = document.getElementById("map");
var ctx = canvas.getContext("2d");
canvas.width = window.innerWidth;
canvas.height = window.innerHeight;
window.onresize = function (evt) {
    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;
    render();
};
var camera = new Camera();
var oldTargetZoom = 1;
var diff = 0;
var mousePos = new Vector2();
function updateMousePos(evt) {
    var rect = canvas.getBoundingClientRect();
    var mp = new Vector2(evt.clientX, evt.clientY);
    var canvasWindowOffset = new Vector2(rect.left, rect.top);
    mousePos = sub(mp, canvasWindowOffset);
}
function renderConnection(p1, p2) {
    if (p1 == null || p2 == null || p1 == p2)
        return;
    var oldLineWidth = ctx.lineWidth;
    ctx.lineWidth = pointInnerRadius;
    ctx.beginPath();
    ctx.moveTo(p1.x, p1.y);
    ctx.lineTo(p2.x, p2.y);
    ctx.stroke();
    ctx.lineWidth = oldLineWidth;
}
var pointOuterRadius = 9, pointInnerRadius = 7;
// The point that is currently moused over.
var hotPoint = null;
var originalDownPos = new Vector2();
var lastMousePos = new Vector2();
var mousedown = false;
var dragStart = new Vector2();
document.body.style.overflow = "hidden";
canvas.onmousedown = function (evt) {
    updateMousePos(evt);
    dragStart = mousePos;
    mousedown = true;
};
canvas.onmouseup = function (evt) {
    updateMousePos(evt);
    mousedown = false;
    camera.pos = add(camera.pos, camera.offset);
    camera.offset = new Vector2();
};
function clamp(val, min, max) {
    return Math.min(Math.max(min, val), max);
}
var minZoomLevel = 1;
var maxZoomLevel = 15;
canvas.onwheel = function (evt) {
    updateMousePos(evt);
    camera.targetZoom = clamp(camera.targetZoom + (evt.deltaY > 0 ? -0.25 : 0.25), minZoomLevel, maxZoomLevel);
    needRedraw = true;
};
canvas.onmouseenter = function (evt) { document.body.style.cursor = "all-scroll"; };
canvas.onmouseleave = function (evt) { canvas.onmouseup(evt); document.body.style.cursor = "auto"; };
canvas.onmousemove = function (evt) {
    updateMousePos(evt);
    document.body.style.cursor = "all-scroll";
    if (mousedown) {
        camera.offset = sub(mousePos, dragStart);
    }
    needRedraw = true;
};
var Point = /** @class */ (function () {
    function Point(name, lon, lat) {
        this.name = name;
        this.longitude = lon;
        this.latitude = lat;
    }
    return Point;
}());
var Connection = /** @class */ (function () {
    function Connection(p1, p2) {
        this.point1 = p1;
        this.point2 = p2;
    }
    return Connection;
}());
var mapRenderData;
var stations = [];
var connections = [];
var minLongLat = new Vector2(22.357122, 44.2125);
var maxLongLat = new Vector2(28.609722, 41.234722);
function init() {
    for (var _i = 0, _a = mapRenderData.stations; _i < _a.length; _i++) {
        var station = _a[_i];
        stations[station.id] = new Point(station.name, station.longitude, station.latitude);
    }
    for (var _b = 0, _c = mapRenderData.connectingLines; _b < _c.length; _b++) {
        var connection = _c[_b];
        connections.push(new Connection(new Vector2(stations[connection.node1Id].longitude, stations[connection.node1Id].latitude), new Vector2(stations[connection.node2Id].longitude, stations[connection.node2Id].latitude)));
    }
}
function renderAllStations() {
    ctx.save();
    ctx.fillStyle = "#4444CC";
    ctx.strokeStyle = "#8888AA";
    ctx.lineWidth = 1;
    for (var _i = 0, _a = Object.keys(stations); _i < _a.length; _i++) {
        var id = _a[_i];
        renderStation(parseInt(id));
    }
    ctx.restore();
}
var zoomPointFactor = 0.5;
function renderStation(id) {
    var normLon = (stations[id].longitude - minLongLat.x) / (maxLongLat.x - minLongLat.x) - 0.5;
    var normLat = (stations[id].latitude - minLongLat.y) / (maxLongLat.y - minLongLat.y) - 0.5;
    ctx.beginPath();
    ctx.arc(normLon * bg.width, normLat * bg.height, clamp(pointOuterRadius / (zoomPointFactor * camera.zoom), 0, pointOuterRadius), 0, Math.PI * 2);
    ctx.stroke();
    ctx.beginPath();
    ctx.arc(normLon * bg.width, normLat * bg.height, clamp(pointInnerRadius / (zoomPointFactor * camera.zoom), 0, pointInnerRadius), 0, Math.PI * 2);
    ctx.fill();
}
var fontsizePixels = 5;
function renderAllStationNames() {
    ctx.save();
    ctx.font = fontsizePixels + "px sans-serif";
    ctx.fillStyle = "black";
    for (var _i = 0, _a = Object.keys(stations); _i < _a.length; _i++) {
        var id = _a[_i];
        var normLon = (stations[id].longitude - minLongLat.x) / (maxLongLat.x - minLongLat.x) - 0.5;
        var normLat = (stations[id].latitude - minLongLat.y) / (maxLongLat.y - minLongLat.y) - 0.5;
        ctx.fillText(stations[id].name, normLon * bg.width - ctx.measureText(stations[id].name).width / 2, normLat * bg.height + 5);
    }
    ctx.restore();
}
function renderConnections() {
}
var needRedraw = true;
var bg = document.getElementById("bg-map");
function render() {
    if (!needRedraw)
        return;
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    ctx.save();
    var camPos = add(camera.pos, camera.offset);
    ctx.translate(camPos.x, camPos.y);
    ctx.scale(camera.zoom, camera.zoom);
    ctx.drawImage(bg, -bg.width / 2, -bg.height / 2);
    renderAllStations();
    if (camera.zoom > 5)
        renderAllStationNames();
    renderConnections();
    ctx.restore();
    needRedraw = false;
}
init();
setInterval(function () {
    camera.zoom = camera.targetZoom;
    render();
}, 16);
//# sourceMappingURL=MapRender.js.map