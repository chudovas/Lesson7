open System

type CalculateStringBuilder<'a>(parseFunction: string -> bool * 'a) = 
    let converter str =
        let (success: bool, value: 'a) = parseFunction(str)
        match success with 
        | true -> Some(value)
        | false -> None 

    member this.Bind(str, f) =
        match converter(str) with
        | None -> None
        | Some(a) -> 
            try 
                f a
            with 
            | _ -> None

    member this.Return(x) =
        Some(x)

type RoundBuilder(acc: int) =
    member this.Bind (x: float, f) =
        try
            f (Math.Round(x, acc))
        with 
        | _ -> None  

    member this.Return (x: float) =
        try
            Some (Math.Round(x, acc))
        with
        | _ -> None 

[<EntryPoint>]
let main argv =
    0
