﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSDotNetCore.WindowFormApp.Querys
{
    public class BlogQuery
    {
        public static string BlogCreate { get; } = @"INSERT INTO [dbo].[Tbl_Blog]
                            ([BlogTitle],
                            [BlogAuthor],
                            [BlogContent])
                            VALUES
                            (@BlogTitle,
                            @BlogAuthor,
                            @BlogContent)";

        public static string BlogRead { get; } = @"SELECT [BlogId]
                            ,[BlogTitle]
                            ,[BlogAuthor]
                            ,[BlogContent]
                            FROM [dbo].[Tbl_Blog]";

        public static string BlogDelete { get; } = @"DELETE FROM [dbo].[Tbl_Blog]
                             WHERE BlogId=@BlogId";

        public static string BlogEdit { get; } = @"SELECT [BlogId]
                            ,[BlogTitle]
                            ,[BlogAuthor]
                            ,[BlogContent]
                            FROM [dbo].[Tbl_Blog] WHERE BlogId=@BlogId";

        public static string BlogUpdate { get; } = @"UPDATE [dbo].[Tbl_Blog]
                              SET [BlogTitle] = @BlogTitle
                             ,[BlogAuthor] = @BlogAuthor
                             ,[BlogContent] = @BlogContent
                             WHERE BlogId=@BlogId";
    }
}
