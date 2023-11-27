// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string entrance = args.Length != 0 ? args[0] : "items";

var cancellationTokenSource = new CancellationTokenSource(new TimeSpan(hours: 0, minutes: 1, seconds: 0));
foreach (var item in Directory.GetFiles(entrance, "*.m4s"))
{
    var bytes = await File.ReadAllBytesAsync(item, cancellationTokenSource.Token);
    string fileName = Path.GetFileName(item);
    var prefix = bytes.Take(3);
    if (!prefix.All(b => b == (byte)0))
    {
        Console.WriteLine("已处理的跳过");
        continue;
    }

    Console.WriteLine("没错，继续执行");
    var z = bytes[3..];
    if (!Directory.Exists("output"))
    {
        Directory.CreateDirectory("output");
    }

    var x = new FileInfo($"output/subtrack_{fileName}");
    using var ms = x.OpenWrite();
    await ms.WriteAsync(z, cancellationTokenSource.Token);
}
