#load "FsFormDraw.fs"
open FsFormDraw
open System.Windows.Forms
open System.Drawing

let formWidth = 500
let formHeight = 400

let width = 400.0f
let height =300.0f

let input = "10 10; 100 10; 100 50; 10 50"

let pts = createPoints input

let scale = calcScaleA pts width height

let fConvert = convertPointByScale scale
let fFlip = flipPointVertical height

let pts' = pts |> Array.map (fConvert >> fFlip)

let paint(e : PaintEventArgs) =
    e.Graphics.DrawLines(new Pen(Color.Black), pts')
    

let mutable form = new Form()
form.Size <- new Size(formWidth, formHeight)

form.Paint.Add paint

form.ShowDialog() |> ignore