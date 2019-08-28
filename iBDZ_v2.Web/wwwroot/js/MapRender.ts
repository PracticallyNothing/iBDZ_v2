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
    /** Target zoom level, used for smooth zooming.*/
    targetZoom: number;

    constructor() {
        this.pos = new Vector2();
        this.offset = new Vector2();
        this.zoom = 1;
        this.targetZoom = 1;
    }
}

var canvas: HTMLCanvasElement = <HTMLCanvasElement>document.getElementById("map")
var ctx: CanvasRenderingContext2D = canvas.getContext("2d")

canvas.width = window.innerWidth
canvas.height = window.innerHeight

window.onresize = function (evt) {
    canvas.width = window.innerWidth
    canvas.height = window.innerHeight
    render()
}

var camera: Camera = new Camera();
let oldTargetZoom: number = 1;
let diff: number = 0;
let mousePos: Vector2 = new Vector2();

function updateMousePos(evt: MouseEvent) {
    let rect: ClientRect | DOMRect = canvas.getBoundingClientRect();
    let mp: Vector2 = new Vector2(evt.clientX, evt.clientY)
    let canvasWindowOffset: Vector2 = new Vector2(rect.left, rect.top)

    mousePos = sub(mp, canvasWindowOffset);
}

function renderConnection(p1: Vector2, p2: Vector2) {
    if (p1 == null || p2 == null || p1 == p2)
        return;

    let oldLineWidth = ctx.lineWidth
    ctx.lineWidth = pointInnerRadius

    ctx.beginPath();
    ctx.moveTo(p1.x, p1.y)
    ctx.lineTo(p2.x, p2.y);
    ctx.stroke();

    ctx.lineWidth = oldLineWidth
}

let pointOuterRadius = 9,
    pointInnerRadius = 7

// The point that is currently moused over.
let hotPoint = null


let originalDownPos: Vector2 = new Vector2();
let lastMousePos: Vector2 = new Vector2();
let mousedown: boolean = false;

let dragStart: Vector2 = new Vector2();

document.body.style.overflow = "hidden";

canvas.onmousedown = function (evt) {
    updateMousePos(evt);
    dragStart = mousePos;
    mousedown = true;
}
canvas.onmouseup = function (evt) {
    updateMousePos(evt);
    mousedown = false;
    camera.pos = add(camera.pos, camera.offset)
    camera.offset = new Vector2();
}

function clamp(val: number, min: number, max: number) {
    return Math.min(Math.max(min, val), max);
}

const minZoomLevel: number = 1;
const maxZoomLevel: number = 15;

canvas.onwheel = function (evt) {
    updateMousePos(evt);

    camera.targetZoom = clamp(
        camera.targetZoom + (evt.deltaY > 0 ? -0.25 : 0.25),
        minZoomLevel,
        maxZoomLevel
    )
    needRedraw = true;
}

canvas.onmouseenter = function (evt) { document.body.style.cursor = "all-scroll"; }
canvas.onmouseleave = function (evt) { canvas.onmouseup(evt); document.body.style.cursor = "auto"; }

canvas.onmousemove = function (evt) {
    updateMousePos(evt);

    document.body.style.cursor = "all-scroll";
    if (mousedown) {
        camera.offset = sub(mousePos, dragStart)
    }

    needRedraw = true;
}

class Point {
    name: string;
    longitude: number;
    latitude: number;

    constructor(name: string, lon: number, lat: number) {
        this.name = name;
        this.longitude = lon;
        this.latitude = lat;
    }
}

class Connection {
    point1: Vector2;
    point2: Vector2;

    constructor(p1: Vector2, p2: Vector2) {
        this.point1 = p1;
        this.point2 = p2;
    }
}

let mapRenderData;
let stations: { [id: number]: Point } = [];
let connections: Connection[] = [];
let minLongLat: Vector2 = new Vector2(22.357122, 44.2125);
let maxLongLat: Vector2 = new Vector2(28.609722, 41.234722);

function init() {
    for (var station of mapRenderData.stations) {
        stations[station.id] = new Point(station.name, station.longitude, station.latitude)
    }

    for (var connection of mapRenderData.connectingLines) {
        connections.push(new Connection(
            new Vector2(
                stations[connection.node1Id].longitude,
                stations[connection.node1Id].latitude),
            new Vector2(
                stations[connection.node2Id].longitude,
                stations[connection.node2Id].latitude)
        ))
    }
}

function renderAllStations() {
    ctx.save();

    ctx.fillStyle = "#4444CC"
    ctx.strokeStyle = "#8888AA"
    ctx.lineWidth = 1

    for (let id of Object.keys(stations)) {
        renderStation(parseInt(id));
    }

    ctx.restore();
}

const zoomPointFactor = 0.5;
function renderStation(id: number) {
    let normLon: number = (stations[id].longitude - minLongLat.x) / (maxLongLat.x - minLongLat.x) - 0.5;
    let normLat: number = (stations[id].latitude - minLongLat.y) / (maxLongLat.y - minLongLat.y) - 0.5;

    ctx.beginPath();
    ctx.arc(
        normLon * bg.width,
        normLat * bg.height,
        clamp(pointOuterRadius / (zoomPointFactor * camera.zoom), 0, pointOuterRadius),
        0, Math.PI * 2
    );
    ctx.stroke();

    ctx.beginPath();
    ctx.arc(
        normLon * bg.width,
        normLat * bg.height,
        clamp(pointInnerRadius / (zoomPointFactor * camera.zoom), 0, pointInnerRadius),
        0, Math.PI * 2
    );
    ctx.fill();
}

let fontsizePixels = 5;

function renderAllStationNames() {
    ctx.save()
    ctx.font = fontsizePixels + "px sans-serif";
    ctx.fillStyle = "black";

    for (let id of Object.keys(stations)) {
        let normLon: number = (stations[id].longitude - minLongLat.x) / (maxLongLat.x - minLongLat.x) - 0.5;
        let normLat: number = (stations[id].latitude - minLongLat.y) / (maxLongLat.y - minLongLat.y) - 0.5;

        ctx.fillText(stations[id].name, normLon * bg.width - ctx.measureText(stations[id].name).width / 2, normLat * bg.height + 5)
    }
    ctx.restore()
}

function renderConnections() {
}

let needRedraw: boolean = true;
let bg: HTMLImageElement = <HTMLImageElement>document.getElementById("bg-map");
function render() {
    if (!needRedraw)
        return;

    ctx.clearRect(0, 0, canvas.width, canvas.height);

    ctx.save();
    let camPos = add(camera.pos, camera.offset);
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
setInterval(() => {
    camera.zoom = camera.targetZoom;
    render();
}, 16);
