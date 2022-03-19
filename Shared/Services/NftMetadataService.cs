﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;
using Lexplorer.Models;

namespace Lexplorer.Services
{
    public class NftMetadataService : IDisposable
    {
        const string BaseUrl = "https://fudgey.mypinata.cloud/ipfs/";

        readonly RestClient _client;

        public NftMetadataService()
        {
            _client = new RestClient(BaseUrl);
        }

        public async Task<NftMetadata> GetMetadata(string link)
        {
            var request = new RestRequest(link);
            try
            {
                var response = await _client.GetAsync(request);
                return JsonConvert.DeserializeObject<NftMetadata>(response.Content);
            }
            catch (System.Net.Sockets.SocketException sex)
            {
                Trace.WriteLine(sex.StackTrace + "\n" + sex.Message);
            }
            catch (JsonReaderException e)
            {
                Trace.WriteLine(e.StackTrace + "\n" + e.Message);
            }
            return null;
            ;
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
