module D3.Definition

open IntelliFactory.WebSharper.InterfaceGenerator

let d3js = Resource "D3" "http://d3js.org/d3.v3.min.js"

let (=>) name o = name => o |> WithSourceName name

let d3 =
    let d3 = Type.New()
    Class "d3"
    |=> d3
    |+> [
        "select" => T<string> ^-> d3
        "selectAll" => T<string> ^-> d3
    ]
    |+> Protocol [
        "data" => T<int []> ^-> d3
        "style" => T<string> * T<string> ^-> T<unit>
        "style" => T<string> * (T<unit> ^-> T<string>) ^-> T<unit>
        "style" => T<string> * (T<int> ^-> T<string>) ^-> T<unit>
        "style" => T<string> * (T<int> * T<int> ^-> T<string>) ^-> T<unit>
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