﻿using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Core.Utilities.Abstracts;
using Core.Utilities.Concrete;
using DataAccess.EntityFramework.Abstracts;
using Dtos.Nodes;
using Entity.Concrete;
using Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class NodeManager : INodeService
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IMapper _mapper;

        public NodeManager(INodeRepository nodeRepository, IMapper mapper)
        {
            _nodeRepository = nodeRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<NodeDto>> AddAsync(NodeCreateDto entity)
        {
            if (await _nodeRepository.AnyAsync(x => x.Name == entity.Name))
            {
                return new ErrorDataResult<NodeDto>(Messages.NodeAlreadyExist);
            }

            var node = await _nodeRepository.AddAsync(_mapper.Map<Node>(entity));

            if (node is null)
                return new ErrorDataResult<NodeDto>(Messages.AddFail);

            return new SuccessDataResult<NodeDto>(_mapper.Map<NodeDto>(node), Messages.AddSuccessfully);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var node = await _nodeRepository.GetByIdAsync(id);

            if (node is null)
                return false;

            var deleteResponse = await _nodeRepository.DeleteAsync(node);

            if (deleteResponse)
                return true;

            return false;
        }

        public async Task<IDataResult<List<NodeListDto>>> GetAllAsync()
        {
            var nodes = await _nodeRepository.GetAllAsync();

            return new SuccessDataResult<List<NodeListDto>>(_mapper.Map<List<NodeListDto>>(nodes), Messages.ListedSuccessfully);
        }

        public async Task<IDataResult<NodeDto>> GetById(Guid id)
        {
            var node = await _nodeRepository.GetByIdAsync(id);

            if (node is null)
                throw new NodeNotFoundException(id);

            return new SuccessDataResult<NodeDto>(_mapper.Map<NodeDto>(node), Messages.FoundSuccessfully);
        }

        public async Task<IDataResult<NodeDto>> UpdateAsync(NodeUpdateDto nodeUpdateDto)
        {
            var nodeToBeUpdated = await _nodeRepository.GetByIdAsync(nodeUpdateDto.Id);
            var mappedNode = _mapper.Map(nodeUpdateDto, nodeToBeUpdated);
            var updatedNode = await _nodeRepository.UpdateAsync(mappedNode);

            if (updatedNode is null)
            {
                return new ErrorDataResult<NodeDto>(Messages.UpdateFail);
            }

            return new SuccessDataResult<NodeDto>(_mapper.Map<NodeDto>(updatedNode), Messages.UpdateSuccessfully);
        }
    }
}
