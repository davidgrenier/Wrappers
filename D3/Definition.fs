module D3.Definition

open IntelliFactory.WebSharper.InterfaceGenerator
type E = IntelliFactory.WebSharper.Dom.Element

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
        "matrix" => T<int [] [] -> unit>
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

let d3 =
    let d3 = Type.New()
    Class "d3"
    |=> d3
    |+> [
        "select" => T<string> ^-> d3
        "select" => T<E> ^-> d3
        "selectAll" => T<string> ^-> d3
        "descending" =? comparator
        "range" => (!? T<int>) * T<int> * (!? T<int>) ^-> T<int []>
    ]
    |+> Protocol [
        "data" => T<int []> ^-> d3
        "style" => T<string> * T<string> ^-> T<unit>
        "style" => T<string> * T<unit -> string> ^-> T<unit>
        "style" => T<string> * T<int -> string> ^-> T<unit>
        "style" => T<string> * T<int * int -> string> ^-> T<unit>
        "transition" => T<unit> ^-> d3
        "append" => T<string> ^-> d3
        Generic - fun t ->
            "attr" => T<string> * t ^-> d3
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
        ]
        Namespace "Wrappers.D3.Resources" [
            d3js
        ]
    ]