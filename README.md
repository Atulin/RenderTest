# RenderTest

Performance comparison between Blazor components, Razor Slices, and plain JSON

## Requirements

* .NET 9
* [UV](https://github.com/astral-sh/uv)
* Python >=3.13

## Run

`dotnet run` to run the API server

`uvx locust` to run the Locust benchmark

## Test description

* Run with 30 concurrent users
* Runtime of 15 minutes
* AMD Ryzen 9 9900X (24) @ 5,65 GHz
* 64 GB DDR5 RAM @ 6400 MT/s

## Endpoints

* `Blazor SSR Endpoint` — endpoint that returns a Blazor component using `RazorComponentResult`
* `JSON Endpoint` — endpoint that returns an object, serialization is left to the framework
* `Razor Slices Endpoint` — endpoint that uses [Razor Slices](https://github.com/DamianEdwards/RazorSlices) to return the same HTML as the Blazor component

## Test conclusions

![img.png](/images/img.png)

* On average, Blazor is **0.12 ms** slower than Slices, and Slices are **0.29 ms** slower than plain JSON
* The best scenario for all endpoints was just **1 ms**
* The worst scenario for JSON and Slices was tied at **6 ms**, with Blazor reaching whopping **29 ms**
* Median, 95th percentile, and 99th percentile are the same for all endpoints