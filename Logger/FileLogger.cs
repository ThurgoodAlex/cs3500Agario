using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger;

/// <summary>
/// Author:    CS3500 Course Staff
/// Date:      Implemented on April 17, 2023
/// Course:    CS 3500, University of Utah, School of Computing
/// Copyright: CS 3500 - This work may not be copied for use in Academic Coursework.
///
/// Toby Armstrong and Alex Thurgood, certify that all 
/// references used in the completion of the assignments are cited 
/// in my README file.
///
/// This class provides the logic for a working File Logger that can be used to help understand/debug a program that makes use of it
/// </summary>
public class FileLogger : ILogger
{
    private readonly string _name;
    private readonly string _fileName;

    /// <summary>
    /// A constructor for the FileLogger that defines the logger itself and the file path that will be logged to 
    /// </summary>
    /// <param name="name"> the name of the file to be logged to </param>
    public FileLogger(string name)
    {
        _name = name;
        _fileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
           + Path.DirectorySeparatorChar
           + $"CS3500-{name}.log";
    }

    public IDisposable BeginScope<TState>(TState state) where TState : notnull
    {
        throw new NotImplementedException();
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        string logMessage = $"{DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss.fff} [{logLevel}] {_name}: {formatter(state, exception)}{Environment.NewLine}";
        File.AppendAllText(_fileName, logMessage);

    }
}

internal class FileLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(categoryName);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}