using System.Collections.Generic;
using VoskApi.Application.Feature.AudioRecognizer.Models;

namespace VoskApi.Application.Feature.AudioRecognizer.Helpers;

public interface IRecognizeResultHelper
{
    string GetSubRip(List<Result> results);
}