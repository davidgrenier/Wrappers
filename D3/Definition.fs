module D3.Definition

open IntelliFactory.WebSharper.InterfaceGenerator
type Node = IntelliFactory.WebSharper.Dom.Node

let d3js = Resource "D3" "http://d3js.org/d3.v3.min.js"

let (=>) name o = name => o |> WithSourceName name
let (=?) name o = name =? o |> WithSourceName name

let comparator =
    Class "d3.comparator"
    |> WithSourceName "d3.comparator"

let chordEntity =
    Class "d3.layout.chord.chordEntity"
    |+> Protocol [
        "index" =? T<int>
        "subindex" =? T<int>
        "startAngle" =? T<float>
        "endAngle" =? T<float>
        "value" =? T<int>
    ]

let chordInfo =
    Class "d3.layout.chord.chordInfo"
    |+> Protocol [
        "source" =? chordEntity
        "target" =? chordEntity
    ]

let chord =
    let chord = Class "d3.layout.chord"
    chord
    |+> [
        Constructor T<unit>
    ]
    |+> Protocol [
        "padding" => T<float> ^-> chord
        "sortSubgroups" => comparator ^-> chord
        "matrix" => T<int [] []> ^-> chord
        "groups" =? T<int []>
        "chords" =? Type.ArrayOf chordInfo
    ]
    |=> Nested [
        chordInfo
        chordEntity
    ]
    |> WithSourceName "chord"

let layout =
    Class "d3.layout"
    |=> Nested [
            chord
        ]
    |> WithSourceName "layout"

let ordinalScale =
    let ordinal = Type.New()
    Class "d3.scale.ordinal"
    |=> ordinal
    |+> [
        Constructor T<unit>
    ]
    |+> Protocol [
        "domain" => T<int []> ^-> ordinal
        "range" => T<int [] -> int -> unit>
    ]
    |> WithSourceName "ordinal"

let scale =
    Class "d3.scale"
    |=> Nested [
            ordinalScale
        ]
    |> WithSourceName "scale"

let selection =
    let selection = Type.New()
    Class "selection"
    |=> selection
    |+> Protocol [
        Generic - fun t -> "data" => Type.ArrayOf t ^-> selection
        Generic - fun t1 t2 -> "data" => (t1 ^-> Type.ArrayOf t2) ^-> selection
        Generic - fun t -> "style" => T<string> * t ^-> selection
        Generic - fun t -> "attr" => T<string> * t ^-> selection
        Generic - fun t -> "text" => (t ^-> T<string>) ^-> selection
        "transition" => T<unit> ^-> selection
        "append" => T<string> ^-> selection
        "selectAll" => T<string> ^-> selection
        "enter" => T<unit> ^-> selection
        "on" => T<string> * T<obj * int -> unit> ^-> selection
        Generic - fun t -> "filter" => (t ^-> T<bool>) ^-> selection
    ] |> WithSourceName "selection"

let arc =
    let arc = Type.New()
    Class "arc"
    |=> arc
    |+> [Constructor T<unit>]
    |+> Protocol [
        "innerRadius" => T<float> ^-> arc
        "outerRadius" => T<float> ^-> arc
    ]
    |> WithSourceName "arc"

let chordGenerator =
    let chord = Type.New()
    Class "d3.svg.chord"
    |=> chord
    |+> [Constructor T<unit>]
    |+> Protocol [
        "radius" => T<float> ^-> chord
    ]
    |> WithSourceName "chord"

let svg =
    Class "d3.svg"
    |=> Nested [
        arc
        chordGenerator
    ]
    |> WithSourceName "svg"

let d3 =
    Class "d3"
    |+> [
        "select" => T<string> ^-> selection
        "select" => T<Node> ^-> selection
        "selectAll" => T<string> ^-> selection
        "descending" =? comparator
        "range" => (!? T<int>) * T<int> * (!? T<int>) ^-> T<int []>
    ]
    |=> Nested [
        layout
        scale
        svg
    ]
    |> WithSourceName "d3"
    |> Requires [d3js]

let assembly =
    Assembly [
        Namespace "Wrappers.D3" [
            d3
            selection
        ]
        Namespace "Wrappers.D3.Resources" [
            d3js
        ]
    ]