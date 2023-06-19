using UralsibTaskTest.DTOs;
using UralsibTaskTest.Entities;


namespace UralsibTaskTest.Common;

public static class CommitMapper
{
    public static Commit ToEntity(CommitDto commitDto)
    {
        return new Commit
        {
            Login = commitDto.Login!,
            UserName = commitDto.Commit.Author.Name,
            DateCreate = commitDto.Commit.Author.Date,
            Message = commitDto.Commit.Message,
            Sha = commitDto.Sha,
            Repository = commitDto.Repository,
            Url = commitDto.Url
        };
    }

    public static CommitDto ToDto(Commit commit)
    {
        return new CommitDto
        {
            Id = commit.Id,
            Sha = commit.Sha,
            Repository = commit.Repository,
            Url = commit.Url,
            Login = commit.Login,
            Commit = new CommitUser
            {
                Message = commit.Message,
                Author = new Author
                {
                    Date = commit.DateCreate,
                    Name = commit.UserName
                }
            }
        };
    }
}