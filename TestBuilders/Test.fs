open System
open Builders
open NUnit.Framework
open FsUnit

[<Test>]
let ``test of string builder 1``() =
    (new CalculateStringBuilder<int64>(Int64.TryParse)) {
        let! x = "1"
        let! y = "H"
        let z = x + y
        return z
    } |> should equal null

[<Test>]
let ``test of string builder 2``() =
    (new CalculateStringBuilder<int32>(Int32.TryParse)) {
        let! x = "1"
        let! y = "2"
        let z = x + y
        return z
    } |> should equal (Some(3))

[<Test>]
let ``test of string builder 3``() =
    let calc = (new CalculateStringBuilder<double>(Double.TryParse)) {
        let! x = "1,5"
        let! y = "2,5"
        let! z = "4,1"
        let res = x + y - z
        return res
    } 

    let abs = Math.Abs(calc.Value - -0.1);
    abs |> should be (lessThan 0.000000001) 

[<Test>]
let ``test of round builder 1``() =
    (new RoundBuilder(4)) {
        let! x = 1.234
        let! y = 0.0
        let z = x / y
        return z
    } |> should equal (Some(infinity))

[<Test>]
let ``test of round builder 2``() =
    (new RoundBuilder(3)) {
        let! a = 2.0 / 12.0
        let! b = 3.5
        return a / b
    } |> should equal (Some(0.048))

[<Test>]
let ``test of round builder 3``() =
    (new RoundBuilder(0)) {
        let! a = 48.0 + 12.12
        let! b = 121.0
        return a / b * 100.0
    } |> should equal (Some(50.))

[<EntryPoint>]
let main argv =
    ``test of string builder 1``()
    ``test of string builder 2``()
    ``test of string builder 3``() 

    ``test of round builder 1``()
    ``test of round builder 2``()
    ``test of round builder 3``()
    0 
