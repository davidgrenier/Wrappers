namespace Website

open IntelliFactory.Html
open IntelliFactory.WebSharper.Sitelets

type Pages =
    | [<CompiledName "">] Home
    | D3

type Examples(page) =
    inherit IntelliFactory.WebSharper.Web.Control()

    [<JS>]
    override this.Body =
        match page with
        | Home -> failwith "Should never happen"
        | D3 -> D3.body()
        :> _

type Site() =
    interface IWebsite<Pages> with
        member __.Actions = []
        member __.Sitelet =
            Sitelet.Infer <| function
                | Home -> Content.Redirect D3
                | page ->
                    PageContent <| fun ctx ->
                        {
                            Page.Default with
                                Title = Some "D3 Examples"
                                Body = [ Div [ new Examples(page) ] ]
                        }

[<assembly: Website(typeof<Site>)>]
do ()