[<JS>]
module Website.D3

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
type d3 = Wrappers.D3.d3

let black (e: Dom.Element) =
    d3.select(e).style("background-color", "black")

let paintPs() =
    d3.selectAll("p")
        .style("color", fun () -> "hsl(" + (random() * 360.0 |> int |> string) + ",100%,50%)")

let alternateGray() =
    d3.selectAll("p")
        .style("background-color", fun (d, i) ->
            match i % 2 with
            | 0 -> "#fff"
            | _ -> "#aaa"
        )

let setTextSize() =
    d3.selectAll("p")
        .data([|4; 8; 15; 16; 23|])
        .style("font-size", fun (d: int) -> string d + "px")

let backgroundToGrey (e: Dom.Element) =
    d3.select(e)
        .transition()
        .style("background-color", "grey")

let matrix =
    [|
        [|11975;  5871; 8916; 2868|]
        [| 1951; 10048; 2060; 6171|]
        [| 8010; 16145; 8090; 8045|]
        [| 1013;   990;  940; 6907|]
    |]

let chord =
    d3.layout
        .chord()
        .padding(0.05)
        .sortSubgroups(d3.descending)
        .matrix(matrix)

let width = 960.0
let height = 500.0
let innerRadius = min width height * 0.41
let outerRadius = innerRadius * 1.1

let fill =
    d3.scale
        .ordinal()
        .domain(d3.range 4)
        .range([|0; 0xffdd89; 0x957244; 0xf26223|])

let svg =
    d3.select("body")
        .append("svg")
        .attr("width", width)
        .attr("height", height)
        .append("g")
        .attr("transform", "translate(" + (string (width / 2.0)) + "," + (string (height / 2.0)) + ")")    

let body() =
    Div [
        Div [
            P [Text "What I'm asking you to entertain is that there is nothing we need to believe on insufficient evidence in order to have deeply ethical and spiritual lives."]
            P [Text "I know of no society in human history that ever suffered because its people became too desirous of their core beliefs."]
            P [Text "Faith enables many of us to endure life's difficulties with an equanimity that would be scarcely conceivable in a world lit only by reason."]
            P [Text "What can be asserted without proof can be dismissed without proof."]
            P [Text "Take the risk of thinking for yourself, much more happiness, truth, beauty, and wisdom will come to you that way."]
            Span []
        ] |>! (fun e -> backgroundToGrey e.Dom)
        Div [
            Text "test"
        ]
    ] |>! (fun e -> black e.Dom)
    |>! OnAfterRender(fun _ ->
        paintPs()
        alternateGray()
        setTextSize()
    )