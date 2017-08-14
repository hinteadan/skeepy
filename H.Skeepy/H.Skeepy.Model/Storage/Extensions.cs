using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Model.Storage
{
    public static class Extensions
    {
        public static DetailsHolderDto ToDto(this DetailsHolder model)
        {
            return ToDto<DetailsHolderDto, DetailsHolder>(model);
        }

        public static IndividualDto ToDto(this Individual model)
        {
            return ToDto<IndividualDto, Individual>(model);
        }

        private static TDto ToDto<TDto, TModel>(TModel model) where TDto : IAmASkeepyDtoFor<TModel>, new()
        {
            var dto = new TDto();
            dto.MorphFromSkeepy(model);
            return dto;
        }
    }
}
