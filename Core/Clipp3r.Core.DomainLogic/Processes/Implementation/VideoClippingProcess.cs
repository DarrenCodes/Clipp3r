using Clipp3r.Core.Common;
using Clipp3r.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Clipp3r.Core.DomainLogic
{
    [RegisterService(typeof(IVideoClippingProcess))]
    class VideoClippingProcess : IVideoClippingProcess
    {
        public void ClipVideo(string filePath, IEnumerable<VideoMomentCaptureDto> videoMomentCaptureList)
        {
            string videoToolPath = $"{AppDomain.CurrentDomain.BaseDirectory}//External//ffmpeg.exe";
            int counter = 0;
            for (int i = 0; i < videoMomentCaptureList.Count(); i += 2)
            {
                string folder = string.Format("{0}//{1}",
                    System.IO.Path.GetDirectoryName(filePath), //Base Directory
                    System.IO.Path.GetFileNameWithoutExtension(filePath)); //Container

                System.IO.Directory.CreateDirectory(folder);

                string newFilePath = string.Format("{0}\\{1}_{2}{3}",
                    folder,
                    videoMomentCaptureList.ElementAt(i).VideoMoment.VideoMomentName, counter++, System.IO.Path.GetExtension(filePath)); //Filename

                long startTime = videoMomentCaptureList.ElementAt(i).CapturedTime / 1000;
                long duration = videoMomentCaptureList.ElementAt(i + 1).CapturedTime / 1000 - startTime;
                string args = $"-ss {startTime} -i \"{filePath}\" -t {duration} -c copy \"{newFilePath}\"";
                Process.Start(videoToolPath, args);
            }
        }
    }
}
