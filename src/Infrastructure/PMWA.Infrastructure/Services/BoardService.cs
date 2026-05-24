using AutoMapper;
using PMWA.Application.Dtos.Board;
using PMWA.Application.Interfaces;
using PMWA.Application.Repositories;

namespace PMWA.Infrastructure.Services
{
    public class BoardService(IBoardRepository boardRepository, IMapper mapper) : IBoardService
    {
        private readonly IBoardRepository _boardRepository = boardRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<BoardDto>?> GetAllAsync()
        {
            var boards = await _boardRepository.GetAllAsync();
            return boards is null ? [] : _mapper.Map<IEnumerable<BoardDto>>(boards);
        }

        public async Task<BoardDto?> GetByIdAsync(Guid id)
        {
            var board = await _boardRepository.GetByIdAsync(id);
            return board is null ? null : _mapper.Map<BoardDto>(board); 
        }

        public async Task<IEnumerable<BoardDto>?> GetByProjectIdAsync(Guid projectId)
        {
            var boards = await _boardRepository.GetByProjectIdAsync(projectId);
            return boards is null ? [] : _mapper.Map<IEnumerable<BoardDto>>(boards);
        }
    }
}
