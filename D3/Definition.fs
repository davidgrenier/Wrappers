module D3.Definition

open IntelliFactory.WebSharper.InterfaceGenerator
type Node = IntelliFactory.WebSharper.Dom.Node

let d3js = Resource "D3" "http://d3js.org/d3.v3.min.js"

let (=>) name o = name => o |> WithSourceName name
let (=?) name o = name =? o |> WithSourceName name

let comparator =
    Class "d3.comparator"
    |> WithSourceName "d3.comparator"

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

let indexed =
    Class "indexed"
    |+> Protocol [
        "index" =? T<int>
    ]
    |> WithSourceName "indexed"

let datum =
    Class "datum"
    |+> Protocol [
        "source" =? indexed
        "target" =? indexed
    ]
    |> WithSourceName "datum"

let selection =
    let selection = Type.New()
    Class "selection"
    |=> selection
    |+> Protocol [
        "data" => T<int []> ^-> selection
        Generic - fun t ->
            "style" => T<string> * t ^-> T<unit>
        "style" => T<string> * T<unit -> string> ^-> T<unit>
        "style" => T<string> * T<int -> string> ^-> T<unit>
        "style" => T<string> * T<int * int -> string> ^-> T<unit>
        "style" => T<string> * (indexed ^-> T<unit>) ^-> selection
        "transition" => T<unit> ^-> selection
        "append" => T<string> ^-> selection
        Generic - fun t ->
            "attr" => T<string> * t ^-> selection
        "selectAll" => T<string> ^-> selection
        "enter" => T<unit> ^-> selection
        "on" => T<string> * T<obj * int -> unit> ^-> selection
        "filter" => (datum ^-> T<bool>) ^-> selection
    ] |> WithSourceName "selection"

let d3 =
    let d3 = Type.New()
    Class "d3"
    |=> d3
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
        ]
    |> WithSourceName "d3"
    |> Requires [d3js]

let assembly =
    Assembly [
        Namespace "Wrappers.D3" [
            d3
            selection
            datum
            indexed
        ]
        Namespace "Wrappers.D3.Resources" [
            d3js
        ]
    ]