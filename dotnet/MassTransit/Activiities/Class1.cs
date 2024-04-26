using MassTransit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitProject.Activiities
{
    public class ValidateImageActivity : IExecuteActivity<ValidateImageArguments>
    {
        public Task<ExecutionResult> Execute(ExecuteContext<ValidateImageArguments> context)
        {
            throw new NotImplementedException();
        }

        async Task<ExecutionResult> Execute(ExecuteContext<DownloadImageArguments> execution)
        {
            DownloadImageArguments args = execution.Arguments;
            string imageSavePath = Path.Combine(args.WorkPath,
            execution.TrackingNumber.ToString());

            await _httpClient.GetAndSave(args.ImageUri, imageSavePath);

            return execution.Completed<DownloadImageLog>(new { ImageSavePath = imageSavePath });
        }
    }

    public class ProcessImageActivity : IActivity<ProcessImageActivityArguments, ProcessImageAcitvityLog>
    {

        Task<CompensationResult> Compensate(CompensateContext<DownloadImageLog> compensation)
        {
            DownloadImageLog log = compensation.Log;
            File.Delete(log.ImageSavePath);

            return compensation.Compensated();
        }
    }


    public class ValidateImageArguments
    {
    }
}
