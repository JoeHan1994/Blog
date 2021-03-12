using Blog.IRepository;
using Blog.IService;
using Blog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public class TypeInfoService:BaseService<TypeInfo>, ITypeInfoService
    {
        private readonly ITypeInfoRepository _typeInfoRepository;
        public TypeInfoService(ITypeInfoRepository typeInfoRepository)
        {
            base._baseRepository = typeInfoRepository;
            _typeInfoRepository = typeInfoRepository;
        }
    }
}
