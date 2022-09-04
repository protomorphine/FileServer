﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileServer.Core.Models;

namespace FileServer.Core.Repositories
{
    public interface IFileRepository
    {
        Task<FileEntity> CreateAsync(FileEntity fileEntity);

        Task<FileEntity> GetAsync(Guid id);

        Task DeleteAsync(FileEntity entity);
    }
}