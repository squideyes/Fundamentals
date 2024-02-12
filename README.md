
![NuGet Version](https://img.shields.io/nuget/v/SquidEyes.Fundamentals)
![Downloads](https://img.shields.io/nuget/dt/squideyes.fundamentals)
![License](https://img.shields.io/github/license/squideyes/Fundamentals)

**SquidEyes.Fundamentals** is a set of helper classes and extension methods.  The solution includes more than 300 unit-tests, and has been **open-sourced under a MIT license** (see License.md for further details).  Even so, the code is mostly for the author's own personal use so there is no documentation on offer, nor does the author have any intent of documenting the code in the near future.

If you want to see what it's all about, please check out the  **UnitTests** and **LoggingDemo** projects. As you will see, the code is rather prosaic (validation extenders, JSON converters, string manipulation methods, etc.).  Even so, there are a number of standouts:

|Class|Description|
|---|---|
|FastArrayReader|A bit like a BinaryReader, but at least 10x faster; for arrays, not streams.|
|HttpHelper|Fetches strings and JSON objects via HTTP(S) endpoints, with easy URL construction like Flurl.Http, but in a lighter-weight object that supports an injectable HttpClientHandler.|
|ConfigHelper|Load and validates configuration values in an easy/safe way|
|CsvEnumerator|A fast, lightweight, super-easy-to-use CSV parser / enumerator that allows CSV files to be read with minimal memory collection pressure.|
|ArgSet|A heterogeneous argument collection that supports twenty data types.|
|SerilogHelper|Helps to build and configure a "standard" Serilog logger, with Seq and Console sinks.  See the **LoggingDemo** project for a comprehensive usage example.|
|SlidingBuffer|A fixed-size generic buffer that supports forward and reverse iteration and indexing.|

Before running the **LoggingDemo**, you may want to install a Seq instance on Docker Desktop.  It's beyond the scope of this README to elaborate on how to do this, but here's the Docker run command I used to setup Seq:

> docker run -d --name seq-dev --restart unless-stopped -p 5341:80  -v "C:\SeqDev:/data" -e ACCEPT_EULA=Y datalust/seq:latest
#
Contributions are always welcome (see [CONTRIBUTING.md](https://github.com/squideyes/Fundamentals/blob/master/CONTRIBUTING.md) for details)

**Supper-Duper Extra-Important Caveat**:  THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.