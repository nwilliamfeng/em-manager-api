﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Data;

namespace EM.Management.Service
{
    public class PointTaskTagService : IPointTaskTagService
    {
        private List<IPointTaskTagRepository> _taskTagRepositories;

        public PointTaskTagService(IEnumerable<IPointTaskTagRepository> taskTagRepositories)
        {
            this._taskTagRepositories = new List<IPointTaskTagRepository>(taskTagRepositories);
        }


        public async Task<JsonResultData<bool>> AddOrUpdate(PointTaskTag taskTag)
        {
          var result = await this.Cache.AddOrUpdate(taskTag);
            if (!result)
                return false.ToJson("添加或更新对象失败");
            result= await this.Db.AddOrUpdate(taskTag);
            return result.ToJson(result ? null : "添加或更新对象失败");
        }

        private IPointTaskTagRepository Cache => this._taskTagRepositories.FirstOrDefault(x => x.IsCache);

        private IPointTaskTagRepository Db => this._taskTagRepositories.FirstOrDefault(x => !x.IsCache);

        public Task<JsonResultData<IEnumerable<PointTaskTag>>> LoadAll()
        {
            throw new NotImplementedException();
        }
    }
}
