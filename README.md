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

`RenderTest` project evaluates three ways of returning data from an API endpoint,
both to see differences between rendering and returning JSON vs. rendered HTML, and
to evaluate possible ways of creating API endpoints that would work well with HTMX

* `Blazor SSR Endpoint` — endpoint that returns a Blazor component using `RazorComponentResult`
* `JSON Endpoint` — endpoint that returns an object, serialization is left to the framework
* `Razor Slices Endpoint` — endpoint that uses [Razor Slices](https://github.com/DamianEdwards/RazorSlices) to return the same HTML as the Blazor component

### Test conclusions

![img.png](/images/img.png)

* On average, Blazor is **0.12 ms** slower than Slices, and Slices are **0.29 ms** slower than plain JSON
* The best scenario for all endpoints was just **1 ms**
* The worst scenario for JSON and Slices was tied at **6 ms**, with Blazor reaching a whopping **29 ms**
* Median, 95th percentile, and 99th percentile are the same for all endpoints

While JSON is a clear winner when it comes to performance, Razor Slices take a nice
spot between that and Blazor components. It seems like RS will be _the_ way to go when
creating an HTMX-based project.

That said, in general, the differences are likely to get lost in network latency,
IO latency, and other longer-running tasks.

## Pages

`RenderTestBis` project evaluates whether Razor Slices + [Immediate.Apis](https://github.com/ImmediatePlatform/Immediate.Apis)
can serve as a viable, AOT-friendly replacement for MVC or Razor Pages

* `Razor Pages` — uses regular Razor Pages page (`Index.cshtml.cs` and `Index.cshtml`)
to render a complete page with five weather forecasts
* `Razor Slices + IA` — uses Razor Slices and Immediate APIs (`Api/Weather.cs` and `Slices/Weather.cshtml`)
to also render a complete page with five weather forecasts

### Test conclusions

![img.png](/images/img_2.png)

* On average, Razor Pages are **0.1 ms** faster than Razor Slices with Immediate APIs combo
* The best scenario for RS+IA **1 ms** slower than Razor Pages (**1 ms**)
* The worst scenario for Razor Pages was **5 ms** slower than RS+IA (**15 ms**)
* Median, 95th percentile, and 99th percentile are the same for both

Overall, the performance differences observed are statistically insignificant and
likely to be lost in network latency, IO operations, and other longer-running tasks.
Additionally, RS+IA is likely to benefit from AOT performance improvements, which
Razor Pages is unlikely to enjoy for the foreseeable future.

While more extensive testing seems prudent, I'd say that RS+IA can be a valid, AOT-friendly
replacement for the usual SSR methods.

## TODO

* Run any tests that can be AOT'd in AOT mode to see how much more performance that can eke out
* Add Blazor SSR test to the Pages tests
* Add a JSON source generator test to the Endpoints test
