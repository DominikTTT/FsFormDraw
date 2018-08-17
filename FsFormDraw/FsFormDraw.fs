module FsFormDraw

open System.Drawing.Drawing2D
open System.Drawing

let createPoint x y =
    new PointF(x, y)

let createPointByString (str : string) =
    let strs = str.Trim().Split()
    let x = System.Single.Parse strs.[0]
    let y = System.Single.Parse strs.[1]
    createPoint x y

let createPoints (str : string) =
    let strs = str.Split(';')
    strs |> Array.map createPointByString


let calcDifferenceMinMax (floats : float32[]) =
    let min = floats |> Array.min
    let max = floats |> Array.max
    max - min

let calcDrawingWidth (pts : PointF[]) =
    let floats = pts |> Array.map (fun pt -> pt.X)
    calcDifferenceMinMax floats


let calcDrawingHeight (pts : PointF[]) =
    let floats = pts |> Array.map (fun pt -> pt.Y)
    calcDifferenceMinMax floats

let calcScale dw dh w h =
    let sw = w / dw
    let sh = h / dh
    if sw > sh then sh else sw

let calcScaleA pts = 
    let dw = calcDrawingWidth pts
    let dh = calcDrawingHeight pts
    calcScale dw dh

let convertPointByScale(s : float32) (pt : PointF) =
    let x = pt.X * s
    let y = pt.Y * s
    createPoint x y

let flipPointVertical (h : float32) (pt : PointF) =
    let x = pt.X
    let y = h - pt.Y
    createPoint x y


let createPath (pts : PointF[]) =
    let b: byte [] = Array.zeroCreate 0
    new GraphicsPath(pts, b)
