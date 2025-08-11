public class ReportGenerator
{
    public string GenerateSales(List<Sale> sales)
    {
        var total = sales.Sum(s => s.Amount);
        return FormatSalesData(sales, total);
    }

    private string FormatSalesData(List<Sale> sales, decimal total)
    {
        var result = $"Total Sales: {total}\n" +
               string.Join("\n", sales.Select(s => $"{s.Item}: {s.Amount}"));

        return result;
    }
}


public class EmailService
{
    public void SendEmail(string email, string emailBody)
    {
        throw new NotImplementedException();
    }
}

public class FileService
{
    public void SaveFile(string file, string formattedData)
    {
        File.WriteAllText(file, formattedData);
    }
}

public class LogService
{
    public void WriteLog(string s)
    {
        Console.WriteLine($"{s} at {DateTime.Now}");
    }
}

public class Sale
{
    public string Item { get; set; }
    public decimal Amount { get; set; }
}

public class ReportService
{
    private readonly ReportGenerator _reportGenerator;
    private readonly EmailService _emailService;
    private readonly FileService _fileService;
    private readonly LogService _logger;

    public ReportService(
        ReportGenerator reportGenerator,
        EmailService emailService,
        FileService fileService,
        LogService logger)
    {
        _reportGenerator = reportGenerator;
        _emailService = emailService;
        _fileService = fileService;
        _logger = logger;
    }

    public void GenerateReport(List<Sale> sales)
    {
        var report = _reportGenerator.GenerateSales(sales);
        _emailService.SendEmail("manager@company.com", $"Report: {report}");
        _fileService.SaveFile("report.txt", report);
        _logger.WriteLog("Report generated");
    }
}

