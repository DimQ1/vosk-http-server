using System;
using System.Collections.Generic;
using System.Linq;
using VoskApi.Application.Feature.AudioRecognizer.Models;

namespace VoskApi.Application.Feature.AudioRecognizer.Helpers;

public class RecognizeResultHelper : IRecognizeResultHelper
{
    public string GetSubRip(List<Result> results)
    {
        var index = 1;
        return string.Join("\r\n", results.Chunk(5).Select(chank =>
            $"{index++}\r\n{TimeSpan.FromMilliseconds(chank.First().Start * 1000):hh\\:mm\\:ss\\,fff} --> {TimeSpan.FromMilliseconds(chank.Last().End * 1000):hh\\:mm\\:ss\\,fff}\r\n{string.Join(" ", chank.Select(ch => ch.Word))}\r\n")
        );
    }
}