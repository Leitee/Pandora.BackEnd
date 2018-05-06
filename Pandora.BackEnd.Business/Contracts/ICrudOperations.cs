using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Business.Contracts
{
    public interface ICrudOperations<TEntity>
    {
        Task<BLResponse<List<TEntity>>> GetAllAsync();
        Task<BLResponse<TEntity>> GetByIdAsync(int pId);
        Task<BLResponse<TEntity>> CreateAsync(TEntity pDto);
        Task<BLResponse<bool>> UpdateAsync(TEntity pDto);
        Task<BLResponse<bool>> DeleteAsync(TEntity pDto);
    }
}
