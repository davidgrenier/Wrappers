[<JS>]
module Website.Samples

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
type d3 = Wrappers.D3.d3

let black (e: Dom.Node) =
    d3.select(e).style("background-color", "black")
    |> ignore

let paintPs() =
    d3.selectAll("p")
        .style("color", fun () -> "hsl(" + (random() * 360.0 |> int |> string) + ",100%,50%)")
    |> ignore

let alternateGray() =
    d3.selectAll("p")
        .style("background-color", fun (_, i) ->
            match i % 2 with
            | 0 -> "#fff"
            | _ -> "#aaa"
        )
    |> ignore

let setTextSize() =
    d3.selectAll("p")
        .data([|4; 8; 15; 16; 23|])
        .style("font-size", fun (d: int) -> string d + "px")
    |> ignore

let backgroundToRed (e: Dom.Node) =
    d3.select(e)
        .style("background-color", "Black")
        .transition()
        .style("background-color", "DarkRed")
    |> ignore

let body() =
    Div [
        Div [
            P [Text "What I'm asking you to entertain is that there is nothing we need to believe on insufficient evidence in order to have deeply ethical and spiritual lives."]
            P [Text "I know of no society in human history that ever suffered because its people became too desirous of their core beliefs."]
            P [Text "Faith enables many of us to endure life's difficulties with an equanimity that would be scarcely conceivable in a world lit only by reason."]
            P [Text "What can be asserted without proof can be dismissed without proof."]
            P [Text "Take the risk of thinking for yourself, much more happiness, truth, beauty, and wisdom will come to you that way."]
            Span []
        ] |>! (fun e -> backgroundToRed e.Dom)
        Div [
            Text "test"
        ]
    ]
    |>! OnAfterRender(fun e ->
        e.Dom.ParentNode.ParentNode |> black
        paintPs()
        alternateGray()
        setTextSize()
    )