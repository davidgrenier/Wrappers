namespace Website

open IntelliFactory.Html
open IntelliFactory.WebSharper.Sitelets

type Page =
    | [<CompiledName "">] Home
    | Chords
    | Samples

type Examples(page) =
    inherit IntelliFactory.WebSharper.Web.Control()

    [<JS>]
    override this.Body =
        match page with
        | Home -> failwith "Should never happen"
        | Samples -> Samples.body()
        | Chords -> Chords.body()
        :> _

type Site() =
    let css = function
        | Home -> failwith "Should never happen"
        | Samples -> []
        | Chords -> [Link [Rel "stylesheet"; Attributes.HRef "chord.css"]]

    interface IWebsite<Page> with
        member __.Actions = []
        member __.Sitelet =
            Sitelet.Infer <| function
                | Home -> Content.Redirect Chords
                | page ->
                    PageContent <| fun ctx ->
                        {
                            Page.Default with
                                Title = Some "D3 Examples"
                                Head = Tags.Meta [CharSet "utf-8"] :: css page
                                Body = [ Div [ new Examples(page) ] ]
                        }

[<assembly: Website(typeof<Site>)>]
do ()