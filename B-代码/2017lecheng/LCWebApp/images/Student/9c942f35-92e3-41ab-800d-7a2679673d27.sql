USE [NewLecheng]
GO
/****** Object:  Table [dbo].[base_country]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_country](
	[country_id] [int] IDENTITY(1,1) NOT NULL,
	[cn_name] [nvarchar](100) NOT NULL,
	[en_name] [nvarchar](100) NOT NULL,
	[money_type] [bigint] NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BASE_COUNTRY] PRIMARY KEY CLUSTERED 
(
	[country_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_employee]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_employee](
	[emp_id] [bigint] IDENTITY(1,1) NOT NULL,
	[emp_no] [bigint] NULL,
	[emp_name] [nvarchar](20) NOT NULL,
	[org_id] [bigint] NULL,
	[ic_card_id] [bigint] NULL,
	[job_id] [bigint] NULL,
	[net_no] [bigint] NOT NULL,
	[photo_url] [nvarchar](200) NOT NULL,
	[idcard_no] [nvarchar](20) NOT NULL,
	[emp_sex] [tinyint] NOT NULL,
	[emp_root] [nvarchar](50) NOT NULL,
	[link_phone] [nvarchar](20) NOT NULL,
	[link_mobile] [nvarchar](15) NOT NULL,
	[link_address] [nvarchar](200) NOT NULL,
	[comp_no] [bigint] NOT NULL,
	[dep_id] [bigint] NOT NULL,
	[bank_name] [nvarchar](50) NOT NULL,
	[bank_no] [nvarchar](50) NOT NULL,
	[date_join] [date] NULL,
	[date_qualify] [date] NULL,
	[date_leave] [date] NULL,
	[salary_join] [money] NOT NULL,
	[salary_qualify] [money] NOT NULL,
	[standard_assess] [bigint] NOT NULL,
	[standard_secrecy] [bigint] NOT NULL,
	[standard__overtime] [bigint] NOT NULL,
	[emp_status] [int] NOT NULL,
	[in_blacklist] [bit] NOT NULL,
	[emp_source] [tinyint] NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](500) NULL,
 CONSTRAINT [PK_tbl_hr_employee_1] PRIMARY KEY CLUSTERED 
(
	[emp_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_iccard_scanin]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_iccard_scanin](
	[ic_card_id] [bigint] IDENTITY(1,1) NOT NULL,
	[net_no] [bigint] NOT NULL,
	[icCard] [nvarchar](50) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[sex] [nvarchar](10) NOT NULL,
	[nation] [nvarchar](50) NOT NULL,
	[born] [date] NOT NULL,
	[address] [nvarchar](100) NOT NULL,
	[grantdept] [nvarchar](100) NOT NULL,
	[start] [date] NOT NULL,
	[end] [date] NOT NULL,
	[create_user_no] [bigint] NOT NULL,
	[create_user_name] [nvarchar](20) NOT NULL,
	[create_time] [datetime] NOT NULL,
 CONSTRAINT [PK_BASE_ICCARD_SCANIN] PRIMARY KEY CLUSTERED 
(
	[ic_card_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_job]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_job](
	[job_id] [bigint] IDENTITY(1,1) NOT NULL,
	[net_no] [bigint] NOT NULL,
	[org_id] [bigint] NOT NULL,
	[asse_id] [bigint] NOT NULL,
	[job_name] [nvarchar](50) NOT NULL,
	[job_status] [bit] NULL,
	[work_begin_time] [time](7) NOT NULL,
	[work_end_time] [time](7) NOT NULL,
	[create_time] [datetime] NULL,
	[create_user_id] [bigint] NULL,
	[edit_time] [datetime] NULL,
	[edit_user_id] [bigint] NULL,
	[del_flag] [bit] NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_tbl_hr_job] PRIMARY KEY CLUSTERED 
(
	[job_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_login_error]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[base_login_error](
	[login_error_id] [bigint] IDENTITY(1,1) NOT NULL,
	[emp_id] [bigint] NULL,
	[user_id] [bigint] NULL,
	[login_ip] [varchar](15) NULL,
	[check_times] [tinyint] NOT NULL,
	[login_date] [datetime] NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
 CONSTRAINT [PK_tbl_user_login_error] PRIMARY KEY CLUSTERED 
(
	[login_error_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[base_login_log]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[base_login_log](
	[login_log_id] [bigint] IDENTITY(1,1) NOT NULL,
	[emp_id] [bigint] NULL,
	[user_id] [bigint] NULL,
	[login_ip] [varchar](15) NULL,
	[login_time] [datetime] NOT NULL,
	[is_success] [bit] NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
 CONSTRAINT [PK_tbl_user_login_log] PRIMARY KEY CLUSTERED 
(
	[login_log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[base_menu_groups]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[base_menu_groups](
	[menu_group_id] [int] IDENTITY(1,1) NOT NULL,
	[group_name] [varchar](50) NOT NULL,
	[net_no] [bigint] NULL,
	[group_sort] [int] NOT NULL,
	[group_status] [bit] NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BASE_MENU_GROUPS] PRIMARY KEY CLUSTERED 
(
	[menu_group_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[base_menus]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[base_menus](
	[menu_id] [int] IDENTITY(1,1) NOT NULL,
	[menu_group_id] [int] NULL,
	[net_no] [bigint] NOT NULL,
	[menugroup_id] [int] NOT NULL,
	[menu_name] [varchar](50) NOT NULL,
	[url_absolute_path] [varchar](50) NULL,
	[menu_sort] [int] NOT NULL,
	[menu_icon] [varchar](50) NULL,
	[menus_status] [bit] NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BASE_MENUS] PRIMARY KEY CLUSTERED 
(
	[menu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[base_org]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_org](
	[org_id] [bigint] IDENTITY(1,1) NOT NULL,
	[net_no] [bigint] NOT NULL,
	[parent_org_id] [bigint] NOT NULL,
	[org_name] [nvarchar](50) NOT NULL,
	[short_name] [nvarchar](50) NULL,
	[only_code] [nvarchar](5) NULL,
	[org_type] [int] NOT NULL,
	[org_status] [int] NOT NULL,
	[org_address] [nvarchar](255) NULL,
	[org_path] [nvarchar](500) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](500) NULL,
 CONSTRAINT [PK_BASE_ORG] PRIMARY KEY CLUSTERED 
(
	[org_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_org_menu_rel]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_org_menu_rel](
	[org_menu_id] [bigint] NOT NULL,
	[org_id] [bigint] NULL,
	[menu_id] [int] NULL,
	[menu_group_id] [int] NULL,
	[net_no] [bigint] NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
 CONSTRAINT [PK_BASE_ORG_MENU_REL] PRIMARY KEY CLUSTERED 
(
	[org_menu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_platform]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_platform](
	[platform_id] [int] IDENTITY(1,1) NOT NULL,
	[platform_name] [nvarchar](100) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BASE_PLATFORM] PRIMARY KEY CLUSTERED 
(
	[platform_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_prod_supp_rel]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_prod_supp_rel](
	[rel_id] [bigint] IDENTITY(1,1) NOT NULL,
	[supp_id] [int] NULL,
	[prod_id] [bigint] NULL,
	[lev_purch] [tinyint] NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BASE_PROD_SUPP_REL] PRIMARY KEY CLUSTERED 
(
	[rel_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_product]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_product](
	[prod_id] [bigint] IDENTITY(10000,1) NOT NULL,
	[prod_class_id] [int] NULL,
	[shop_id] [int] NOT NULL,
	[brand_id] [int] NOT NULL,
	[pic_url] [nvarchar](500) NOT NULL,
	[price_us] [decimal](8, 4) NOT NULL,
	[price_cn] [decimal](8, 4) NOT NULL,
	[price_jp] [decimal](8, 4) NOT NULL,
	[prod_property] [int] NOT NULL,
	[ex_type] [int] NOT NULL,
	[prod_status] [int] NOT NULL,
	[prod_title] [nvarchar](200) NULL,
	[title_en] [nvarchar](500) NULL,
	[title_jp] [nvarchar](500) NULL,
	[pre_weight] [int] NULL,
	[real_weight] [int] NULL,
	[prod_url] [nvarchar](500) NULL,
	[prod_model] [nvarchar](50) NULL,
	[prod_size] [nvarchar](50) NULL,
	[prod_code] [nvarchar](50) NULL,
	[prod_tag] [nvarchar](200) NULL,
	[prod_spec] [nvarchar](200) NULL,
	[prod_picpath] [nvarchar](300) NULL,
	[prod_developer] [nvarchar](200) NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BASE_PRODUCT] PRIMARY KEY CLUSTERED 
(
	[prod_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_product_imgs]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_product_imgs](
	[img_id] [bigint] IDENTITY(1,1) NOT NULL,
	[prod_id] [bigint] NULL,
	[prod_model] [nvarchar](50) NULL,
	[pic_url] [nvarchar](500) NULL,
	[pic_describe] [nvarchar](500) NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BASE_PRODUCT_IMGS] PRIMARY KEY CLUSTERED 
(
	[img_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_productclass]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_productclass](
	[prod_class_id] [int] IDENTITY(1,1) NOT NULL,
	[parent_id] [int] NULL,
	[class_name] [nvarchar](100) NOT NULL,
	[class_icon] [nvarchar](200) NULL,
	[class_sort] [int] NULL,
	[create_time] [datetime] NOT NULL CONSTRAINT [DF__base_prod__creat__395884C4]  DEFAULT (getdate()),
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NULL CONSTRAINT [DF__base_prod__edit___3A4CA8FD]  DEFAULT (getdate()),
	[edit_user_id] [bigint] NULL,
	[del_flag] [bit] NOT NULL CONSTRAINT [DF__base_prod__del_f__3B40CD36]  DEFAULT ((1)),
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BASE_PRODUCTCLASS] PRIMARY KEY CLUSTERED 
(
	[prod_class_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_role]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_role](
	[role_id] [bigint] IDENTITY(1,1) NOT NULL,
	[org_id] [bigint] NULL,
	[net_no] [bigint] NOT NULL,
	[role_name] [nvarchar](50) NOT NULL,
	[role_status] [bit] NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BASE_ROLE] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_role_menu_rel]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_role_menu_rel](
	[role_menu_id] [bigint] IDENTITY(1,1) NOT NULL,
	[role_id] [bigint] NULL,
	[menu_id] [int] NULL,
	[menu_group_id] [int] NULL,
	[net_no] [bigint] NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
 CONSTRAINT [PK_BASE_ROLE_MENU_REL] PRIMARY KEY CLUSTERED 
(
	[role_menu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_role_user_rel]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_role_user_rel](
	[role_user_id] [bigint] IDENTITY(1,1) NOT NULL,
	[role_id] [bigint] NULL,
	[user_id] [bigint] NULL,
	[net_no] [bigint] NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
 CONSTRAINT [PK_BASE_ROLE_USER_REL] PRIMARY KEY CLUSTERED 
(
	[role_user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_shop]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_shop](
	[shop_id] [bigint] IDENTITY(1,1) NOT NULL,
	[platform_id] [int] NULL,
	[country_id] [int] NULL,
	[shop_name] [nvarchar](100) NULL,
	[shop_account] [nvarchar](100) NULL,
	[settlm_currency] [bigint] NULL,
	[platform_lrish] [decimal](8, 4) NULL,
	[shop_status] [bit] NULL,
	[shop_telephone] [nvarchar](40) NULL,
	[shop_zipcode] [nvarchar](20) NULL,
	[shop_address] [nvarchar](200) NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BASE_SHOP] PRIMARY KEY CLUSTERED 
(
	[shop_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_supplier]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_supplier](
	[supp_id] [int] IDENTITY(1,1) NOT NULL,
	[supp_code] [nvarchar](200) NOT NULL,
	[purc_sourceid] [int] NOT NULL,
	[purc_mode] [int] NOT NULL,
	[supp_name] [nvarchar](200) NOT NULL,
	[supp_url] [nvarchar](500) NOT NULL,
	[purc_priority] [tinyint] NULL,
	[is_grade] [bit] NULL,
	[create_time] [datetime] NOT NULL CONSTRAINT [DF__base_supp__creat__108B795B]  DEFAULT (getdate()),
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL CONSTRAINT [DF__base_supp__edit___117F9D94]  DEFAULT (getdate()),
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL CONSTRAINT [DF__base_supp__del_f__1273C1CD]  DEFAULT ((1)),
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BASE_SUPPLIER] PRIMARY KEY CLUSTERED 
(
	[supp_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_user_menu_rel]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_user_menu_rel](
	[user_menu_id] [bigint] IDENTITY(1,1) NOT NULL,
	[menu_id] [int] NULL,
	[menu_group_id] [int] NULL,
	[user_id] [bigint] NULL,
	[role_menu_id] [bigint] NULL,
	[net_no] [bigint] NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
 CONSTRAINT [PK_BASE_USER_MENU_REL] PRIMARY KEY CLUSTERED 
(
	[user_menu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_users]    Script Date: 2017/4/28 17:59:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[base_users](
	[user_id] [bigint] IDENTITY(1,1) NOT NULL,
	[emp_id] [bigint] NOT NULL,
	[emp_no] [bigint] NULL,
	[user_name] [nvarchar](20) NOT NULL,
	[user_pwd] [varchar](50) NOT NULL,
	[net_no] [bigint] NOT NULL,
	[org_id] [bigint] NOT NULL,
	[salt] [varchar](10) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[real_name] [varchar](50) NULL,
	[sex] [nvarchar](10) NOT NULL,
	[telphone] [nvarchar](15) NOT NULL,
	[mobile] [varchar](15) NOT NULL,
	[user_status] [bit] NOT NULL,
	[job_id] [bigint] NULL,
	[duty] [tinyint] NOT NULL,
	[position] [tinyint] NOT NULL,
	[op_ip] [varchar](15) NOT NULL,
	[open_time] [datetime] NULL,
	[disable_time] [datetime] NULL,
	[token_code] [varchar](50) NULL,
	[token_expiry_time] [datetime] NULL,
	[sto_user_id] [varchar](36) NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_tbl_users] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[base_country] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_country] ADD  DEFAULT (getdate()) FOR [edit_time]
GO
ALTER TABLE [dbo].[base_country] ADD  DEFAULT ((1)) FOR [del_flag]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ((0)) FOR [emp_no]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ('') FOR [emp_name]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ('') FOR [photo_url]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ('') FOR [idcard_no]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ('') FOR [emp_root]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ('') FOR [link_phone]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ('') FOR [link_mobile]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ('') FOR [link_address]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ((0)) FOR [comp_no]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ((0)) FOR [dep_id]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ('') FOR [bank_name]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ('') FOR [bank_no]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ((0)) FOR [salary_join]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ((0)) FOR [salary_qualify]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ((0)) FOR [standard_assess]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ((0)) FOR [standard_secrecy]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ((0)) FOR [standard__overtime]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ((0)) FOR [emp_status]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ((0)) FOR [in_blacklist]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT (getdate()) FOR [edit_time]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ((1)) FOR [del_flag]
GO
ALTER TABLE [dbo].[base_employee] ADD  DEFAULT ('') FOR [remark]
GO
ALTER TABLE [dbo].[base_iccard_scanin] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_job] ADD  DEFAULT ((0)) FOR [asse_id]
GO
ALTER TABLE [dbo].[base_job] ADD  DEFAULT ((1)) FOR [job_status]
GO
ALTER TABLE [dbo].[base_job] ADD  DEFAULT ('') FOR [work_begin_time]
GO
ALTER TABLE [dbo].[base_job] ADD  DEFAULT ('') FOR [work_end_time]
GO
ALTER TABLE [dbo].[base_job] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_job] ADD  DEFAULT (getdate()) FOR [edit_time]
GO
ALTER TABLE [dbo].[base_job] ADD  DEFAULT ((1)) FOR [del_flag]
GO
ALTER TABLE [dbo].[base_job] ADD  DEFAULT ('') FOR [remark]
GO
ALTER TABLE [dbo].[base_login_error] ADD  DEFAULT ('') FOR [login_ip]
GO
ALTER TABLE [dbo].[base_login_error] ADD  DEFAULT ((1)) FOR [check_times]
GO
ALTER TABLE [dbo].[base_login_error] ADD  DEFAULT (getdate()) FOR [login_date]
GO
ALTER TABLE [dbo].[base_login_error] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_login_log] ADD  DEFAULT ('') FOR [login_ip]
GO
ALTER TABLE [dbo].[base_login_log] ADD  DEFAULT (getdate()) FOR [login_time]
GO
ALTER TABLE [dbo].[base_login_log] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_menu_groups] ADD  DEFAULT ((1)) FOR [group_status]
GO
ALTER TABLE [dbo].[base_menu_groups] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_menu_groups] ADD  DEFAULT (getdate()) FOR [edit_time]
GO
ALTER TABLE [dbo].[base_menu_groups] ADD  DEFAULT ((1)) FOR [del_flag]
GO
ALTER TABLE [dbo].[base_menu_groups] ADD  DEFAULT ('') FOR [remark]
GO
ALTER TABLE [dbo].[base_menus] ADD  DEFAULT ((1)) FOR [menu_sort]
GO
ALTER TABLE [dbo].[base_menus] ADD  DEFAULT ((1)) FOR [menus_status]
GO
ALTER TABLE [dbo].[base_menus] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_menus] ADD  DEFAULT (getdate()) FOR [edit_time]
GO
ALTER TABLE [dbo].[base_menus] ADD  DEFAULT ((1)) FOR [del_flag]
GO
ALTER TABLE [dbo].[base_menus] ADD  DEFAULT ('') FOR [remark]
GO
ALTER TABLE [dbo].[base_org] ADD  DEFAULT ((0)) FOR [parent_org_id]
GO
ALTER TABLE [dbo].[base_org] ADD  DEFAULT ((0)) FOR [org_type]
GO
ALTER TABLE [dbo].[base_org] ADD  DEFAULT ((1)) FOR [org_status]
GO
ALTER TABLE [dbo].[base_org] ADD  DEFAULT ('') FOR [org_path]
GO
ALTER TABLE [dbo].[base_org] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_org] ADD  DEFAULT (getdate()) FOR [edit_time]
GO
ALTER TABLE [dbo].[base_org] ADD  DEFAULT ((1)) FOR [del_flag]
GO
ALTER TABLE [dbo].[base_org] ADD  DEFAULT ('') FOR [remark]
GO
ALTER TABLE [dbo].[base_org_menu_rel] ADD  DEFAULT ('') FOR [net_no]
GO
ALTER TABLE [dbo].[base_org_menu_rel] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_org_menu_rel] ADD  DEFAULT (getdate()) FOR [edit_time]
GO
ALTER TABLE [dbo].[base_org_menu_rel] ADD  DEFAULT ((1)) FOR [del_flag]
GO
ALTER TABLE [dbo].[base_platform] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_platform] ADD  DEFAULT (getdate()) FOR [edit_time]
GO
ALTER TABLE [dbo].[base_platform] ADD  DEFAULT ((1)) FOR [del_flag]
GO
ALTER TABLE [dbo].[base_prod_supp_rel] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_prod_supp_rel] ADD  DEFAULT (getdate()) FOR [edit_time]
GO
ALTER TABLE [dbo].[base_prod_supp_rel] ADD  DEFAULT ((1)) FOR [del_flag]
GO
ALTER TABLE [dbo].[base_product] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_product] ADD  DEFAULT (getdate()) FOR [edit_time]
GO
ALTER TABLE [dbo].[base_product] ADD  DEFAULT ((1)) FOR [del_flag]
GO
ALTER TABLE [dbo].[base_product_imgs] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_product_imgs] ADD  DEFAULT (getdate()) FOR [edit_time]
GO
ALTER TABLE [dbo].[base_product_imgs] ADD  DEFAULT ((1)) FOR [del_flag]
GO
ALTER TABLE [dbo].[base_role] ADD  DEFAULT ((1)) FOR [role_status]
GO
ALTER TABLE [dbo].[base_role] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_role] ADD  DEFAULT (getdate()) FOR [edit_time]
GO
ALTER TABLE [dbo].[base_role] ADD  DEFAULT ((1)) FOR [del_flag]
GO
ALTER TABLE [dbo].[base_role_menu_rel] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_role_menu_rel] ADD  DEFAULT (getdate()) FOR [edit_time]
GO
ALTER TABLE [dbo].[base_role_menu_rel] ADD  DEFAULT ((1)) FOR [del_flag]
GO
ALTER TABLE [dbo].[base_role_user_rel] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_role_user_rel] ADD  DEFAULT (getdate()) FOR [edit_time]
GO
ALTER TABLE [dbo].[base_role_user_rel] ADD  DEFAULT ((1)) FOR [del_flag]
GO
ALTER TABLE [dbo].[base_shop] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_shop] ADD  DEFAULT (getdate()) FOR [edit_time]
GO
ALTER TABLE [dbo].[base_shop] ADD  DEFAULT ((1)) FOR [del_flag]
GO
ALTER TABLE [dbo].[base_user_menu_rel] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_user_menu_rel] ADD  DEFAULT (getdate()) FOR [edit_time]
GO
ALTER TABLE [dbo].[base_user_menu_rel] ADD  DEFAULT ((1)) FOR [del_flag]
GO
ALTER TABLE [dbo].[base_users] ADD  DEFAULT ('') FOR [user_name]
GO
ALTER TABLE [dbo].[base_users] ADD  DEFAULT ((0)) FOR [net_no]
GO
ALTER TABLE [dbo].[base_users] ADD  DEFAULT ((0)) FOR [org_id]
GO
ALTER TABLE [dbo].[base_users] ADD  DEFAULT ('') FOR [email]
GO
ALTER TABLE [dbo].[base_users] ADD  DEFAULT ('保密') FOR [sex]
GO
ALTER TABLE [dbo].[base_users] ADD  DEFAULT ('') FOR [telphone]
GO
ALTER TABLE [dbo].[base_users] ADD  DEFAULT ('') FOR [mobile]
GO
ALTER TABLE [dbo].[base_users] ADD  DEFAULT ((1)) FOR [user_status]
GO
ALTER TABLE [dbo].[base_users] ADD  DEFAULT ((2)) FOR [duty]
GO
ALTER TABLE [dbo].[base_users] ADD  DEFAULT ('') FOR [position]
GO
ALTER TABLE [dbo].[base_users] ADD  DEFAULT ('') FOR [op_ip]
GO
ALTER TABLE [dbo].[base_users] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[base_users] ADD  DEFAULT (getdate()) FOR [edit_time]
GO
ALTER TABLE [dbo].[base_users] ADD  DEFAULT ((1)) FOR [del_flag]
GO
ALTER TABLE [dbo].[base_users] ADD  DEFAULT ('') FOR [remark]
GO
ALTER TABLE [dbo].[base_login_error]  WITH CHECK ADD  CONSTRAINT [FK_BASE_LOG_REFERENCE_BASE_EMP] FOREIGN KEY([emp_id])
REFERENCES [dbo].[base_employee] ([emp_id])
GO
ALTER TABLE [dbo].[base_login_error] CHECK CONSTRAINT [FK_BASE_LOG_REFERENCE_BASE_EMP]
GO
ALTER TABLE [dbo].[base_menus]  WITH CHECK ADD  CONSTRAINT [FK_47] FOREIGN KEY([menu_group_id])
REFERENCES [dbo].[base_menu_groups] ([menu_group_id])
GO
ALTER TABLE [dbo].[base_menus] CHECK CONSTRAINT [FK_47]
GO
ALTER TABLE [dbo].[base_org_menu_rel]  WITH CHECK ADD  CONSTRAINT [FK_49] FOREIGN KEY([menu_group_id])
REFERENCES [dbo].[base_menu_groups] ([menu_group_id])
GO
ALTER TABLE [dbo].[base_org_menu_rel] CHECK CONSTRAINT [FK_49]
GO
ALTER TABLE [dbo].[base_org_menu_rel]  WITH CHECK ADD  CONSTRAINT [FK_50] FOREIGN KEY([menu_id])
REFERENCES [dbo].[base_menus] ([menu_id])
GO
ALTER TABLE [dbo].[base_org_menu_rel] CHECK CONSTRAINT [FK_50]
GO
ALTER TABLE [dbo].[base_org_menu_rel]  WITH CHECK ADD  CONSTRAINT [FK_51] FOREIGN KEY([org_id])
REFERENCES [dbo].[base_org] ([org_id])
GO
ALTER TABLE [dbo].[base_org_menu_rel] CHECK CONSTRAINT [FK_51]
GO
ALTER TABLE [dbo].[base_prod_supp_rel]  WITH CHECK ADD  CONSTRAINT [FK_BASE_PRO_REFERENCE_BASE_SUP] FOREIGN KEY([supp_id])
REFERENCES [dbo].[base_supplier] ([supp_id])
GO
ALTER TABLE [dbo].[base_prod_supp_rel] CHECK CONSTRAINT [FK_BASE_PRO_REFERENCE_BASE_SUP]
GO
ALTER TABLE [dbo].[base_product_imgs]  WITH CHECK ADD  CONSTRAINT [FK_BASE_PRO_REFERENCE_BASE_PRO] FOREIGN KEY([prod_id])
REFERENCES [dbo].[base_product] ([prod_id])
GO
ALTER TABLE [dbo].[base_product_imgs] CHECK CONSTRAINT [FK_BASE_PRO_REFERENCE_BASE_PRO]
GO
ALTER TABLE [dbo].[base_role]  WITH CHECK ADD  CONSTRAINT [FK_52] FOREIGN KEY([org_id])
REFERENCES [dbo].[base_org] ([org_id])
GO
ALTER TABLE [dbo].[base_role] CHECK CONSTRAINT [FK_52]
GO
ALTER TABLE [dbo].[base_role_menu_rel]  WITH CHECK ADD  CONSTRAINT [FK_43] FOREIGN KEY([role_id])
REFERENCES [dbo].[base_role] ([role_id])
GO
ALTER TABLE [dbo].[base_role_menu_rel] CHECK CONSTRAINT [FK_43]
GO
ALTER TABLE [dbo].[base_role_menu_rel]  WITH CHECK ADD  CONSTRAINT [FK_44] FOREIGN KEY([menu_id])
REFERENCES [dbo].[base_menus] ([menu_id])
GO
ALTER TABLE [dbo].[base_role_menu_rel] CHECK CONSTRAINT [FK_44]
GO
ALTER TABLE [dbo].[base_role_menu_rel]  WITH CHECK ADD  CONSTRAINT [FK_45] FOREIGN KEY([menu_group_id])
REFERENCES [dbo].[base_menu_groups] ([menu_group_id])
GO
ALTER TABLE [dbo].[base_role_menu_rel] CHECK CONSTRAINT [FK_45]
GO
ALTER TABLE [dbo].[base_role_user_rel]  WITH CHECK ADD  CONSTRAINT [FK_55] FOREIGN KEY([role_id])
REFERENCES [dbo].[base_role] ([role_id])
GO
ALTER TABLE [dbo].[base_role_user_rel] CHECK CONSTRAINT [FK_55]
GO
ALTER TABLE [dbo].[base_shop]  WITH CHECK ADD  CONSTRAINT [FK_BASE_SHO_REFERENCE_BASE_PLA] FOREIGN KEY([platform_id])
REFERENCES [dbo].[base_platform] ([platform_id])
GO
ALTER TABLE [dbo].[base_shop] CHECK CONSTRAINT [FK_BASE_SHO_REFERENCE_BASE_PLA]
GO
ALTER TABLE [dbo].[base_user_menu_rel]  WITH CHECK ADD  CONSTRAINT [FK_46] FOREIGN KEY([menu_id])
REFERENCES [dbo].[base_menus] ([menu_id])
GO
ALTER TABLE [dbo].[base_user_menu_rel] CHECK CONSTRAINT [FK_46]
GO
ALTER TABLE [dbo].[base_user_menu_rel]  WITH CHECK ADD  CONSTRAINT [FK_48] FOREIGN KEY([menu_group_id])
REFERENCES [dbo].[base_menu_groups] ([menu_group_id])
GO
ALTER TABLE [dbo].[base_user_menu_rel] CHECK CONSTRAINT [FK_48]
GO
ALTER TABLE [dbo].[base_user_menu_rel]  WITH CHECK ADD  CONSTRAINT [FK_BASE_USE_REFERENCE_BASE_ROL] FOREIGN KEY([role_menu_id])
REFERENCES [dbo].[base_role_menu_rel] ([role_menu_id])
GO
ALTER TABLE [dbo].[base_user_menu_rel] CHECK CONSTRAINT [FK_BASE_USE_REFERENCE_BASE_ROL]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'国家id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_country', @level2type=N'COLUMN',@level2name=N'country_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'中文国家名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_country', @level2type=N'COLUMN',@level2name=N'cn_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'英文国家名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_country', @level2type=N'COLUMN',@level2name=N'en_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'取系统字典表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_country', @level2type=N'COLUMN',@level2name=N'money_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_country', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_country', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_country', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_country', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已删除;1:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_country', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_country', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_country', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_country', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础数据—国家表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_country'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'emp_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工工号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'emp_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'emp_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'org_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工卡ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'ic_card_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'岗位ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'job_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属网点(公司)表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'net_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登记照地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'photo_url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'身份证号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'idcard_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对应字典表中:性别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'emp_sex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'籍贯' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'emp_root'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'link_phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'link_mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'link_address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'comp_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'dep_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对应字典中的银行' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'bank_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'银行卡号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'bank_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入职日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'date_join'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转正日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'date_qualify'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'离职日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'date_leave'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'默认为0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'salary_join'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转正工资' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'salary_qualify'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'考核工资标准' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'standard_assess'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'保密工资标准' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'standard_secrecy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'加班工资标准' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'standard__overtime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'从字典:员工状态中获取' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'emp_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否在员工黑名单中' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'in_blacklist'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'例如校招等,从字典:员工来源 获取' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'emp_source'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.有效;0.已删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_employee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_iccard_scanin', @level2type=N'COLUMN',@level2name=N'ic_card_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网点(公司)ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_iccard_scanin', @level2type=N'COLUMN',@level2name=N'net_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'身份证号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_iccard_scanin', @level2type=N'COLUMN',@level2name=N'icCard'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_iccard_scanin', @level2type=N'COLUMN',@level2name=N'name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_iccard_scanin', @level2type=N'COLUMN',@level2name=N'sex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'籍贯' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_iccard_scanin', @level2type=N'COLUMN',@level2name=N'nation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出生日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_iccard_scanin', @level2type=N'COLUMN',@level2name=N'born'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'address' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_iccard_scanin', @level2type=N'COLUMN',@level2name=N'address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发证机关' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_iccard_scanin', @level2type=N'COLUMN',@level2name=N'grantdept'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'有效期开始日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_iccard_scanin', @level2type=N'COLUMN',@level2name=N'start'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'有效期结束日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_iccard_scanin', @level2type=N'COLUMN',@level2name=N'end'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'create_user_no' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_iccard_scanin', @level2type=N'COLUMN',@level2name=N'create_user_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'create_user_name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_iccard_scanin', @level2type=N'COLUMN',@level2name=N'create_user_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'create_time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_iccard_scanin', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'身份证扫描表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_iccard_scanin'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'岗位ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_job', @level2type=N'COLUMN',@level2name=N'job_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属网点(公司)ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_job', @level2type=N'COLUMN',@level2name=N'net_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属组织架构ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_job', @level2type=N'COLUMN',@level2name=N'org_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关联tbl_hr_Assessment表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_job', @level2type=N'COLUMN',@level2name=N'asse_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'岗位名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_job', @level2type=N'COLUMN',@level2name=N'job_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：停用；1：正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_job', @level2type=N'COLUMN',@level2name=N'job_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上班时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_job', @level2type=N'COLUMN',@level2name=N'work_begin_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下班时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_job', @level2type=N'COLUMN',@level2name=N'work_end_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_job', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_job', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_job', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_job', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.有效;0.已删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_job', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_job', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_job', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_job', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'岗位职务表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_job'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录错误日志ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_login_error', @level2type=N'COLUMN',@level2name=N'login_error_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录员工ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_login_error', @level2type=N'COLUMN',@level2name=N'emp_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_login_error', @level2type=N'COLUMN',@level2name=N'user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录IP' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_login_error', @level2type=N'COLUMN',@level2name=N'login_ip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'超过5次设置10分钟的缓冲时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_login_error', @level2type=N'COLUMN',@level2name=N'check_times'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最近一次登录错误时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_login_error', @level2type=N'COLUMN',@level2name=N'login_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_login_error', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_login_error', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户错误登录日志表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_login_error'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'正确登录日志ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_login_log', @level2type=N'COLUMN',@level2name=N'login_log_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录员工ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_login_log', @level2type=N'COLUMN',@level2name=N'emp_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_login_log', @level2type=N'COLUMN',@level2name=N'user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录IP' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_login_log', @level2type=N'COLUMN',@level2name=N'login_ip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_login_log', @level2type=N'COLUMN',@level2name=N'login_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0.成功;1.失败' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_login_log', @level2type=N'COLUMN',@level2name=N'is_success'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_login_log', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_login_log', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户正确登录日志表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_login_log'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单分组ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menu_groups', @level2type=N'COLUMN',@level2name=N'menu_group_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单组名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menu_groups', @level2type=N'COLUMN',@level2name=N'group_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网点(公司)ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menu_groups', @level2type=N'COLUMN',@level2name=N'net_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单组排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menu_groups', @level2type=N'COLUMN',@level2name=N'group_sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：停用；1：正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menu_groups', @level2type=N'COLUMN',@level2name=N'group_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menu_groups', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menu_groups', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menu_groups', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menu_groups', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.有效;0.已删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menu_groups', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menu_groups', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menu_groups', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menu_groups', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单分组表(来自GLOBAL)库' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menu_groups'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单权限ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menus', @level2type=N'COLUMN',@level2name=N'menu_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单分组ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menus', @level2type=N'COLUMN',@level2name=N'menu_group_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网点(公司)ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menus', @level2type=N'COLUMN',@level2name=N'net_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单组自增编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menus', @level2type=N'COLUMN',@level2name=N'menugroup_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menus', @level2type=N'COLUMN',@level2name=N'menu_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'访问地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menus', @level2type=N'COLUMN',@level2name=N'url_absolute_path'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menus', @level2type=N'COLUMN',@level2name=N'menu_sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单icon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menus', @level2type=N'COLUMN',@level2name=N'menu_icon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:停用;1.正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menus', @level2type=N'COLUMN',@level2name=N'menus_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menus', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menus', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menus', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menus', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.有效;0.已删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menus', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menus', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menus', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menus', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单表(来自GLOBAL库)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_menus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'org_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网点(公司)表主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'net_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父节点组织' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'parent_org_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'org_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'简称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'short_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'全网唯一,例如YWSTO' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'only_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1：公司，2：部门' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'org_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：停用；1：正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'org_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'org_address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织权限路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'org_path'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.有效;0.已删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织架构表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织与菜单关系ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org_menu_rel', @level2type=N'COLUMN',@level2name=N'org_menu_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org_menu_rel', @level2type=N'COLUMN',@level2name=N'org_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单权限ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org_menu_rel', @level2type=N'COLUMN',@level2name=N'menu_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单分组ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org_menu_rel', @level2type=N'COLUMN',@level2name=N'menu_group_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网点(公司)编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org_menu_rel', @level2type=N'COLUMN',@level2name=N'net_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org_menu_rel', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org_menu_rel', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org_menu_rel', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org_menu_rel', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.有效;0.已删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org_menu_rel', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org_menu_rel', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org_menu_rel', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织架构与菜单关系表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_org_menu_rel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_platform', @level2type=N'COLUMN',@level2name=N'platform_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_platform', @level2type=N'COLUMN',@level2name=N'platform_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_platform', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_platform', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_platform', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_platform', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已删除;1:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_platform', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_platform', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_platform', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_platform', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础数据-平台表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_platform'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_supp_rel', @level2type=N'COLUMN',@level2name=N'rel_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'供应商id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_supp_rel', @level2type=N'COLUMN',@level2name=N'supp_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_supp_rel', @level2type=N'COLUMN',@level2name=N'prod_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购等级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_supp_rel', @level2type=N'COLUMN',@level2name=N'lev_purch'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_supp_rel', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_supp_rel', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_supp_rel', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_supp_rel', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已删除;1:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_supp_rel', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_supp_rel', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_supp_rel', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_supp_rel', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关系表-商品供应商关系表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_supp_rel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'prod_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'prod_class_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属店铺id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'shop_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'暂无' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'brand_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品主图片地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'pic_url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参考价格（美元）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'price_us'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参考价格（人民币）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'price_cn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参考价格（日元）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'price_jp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0未指定，1普货，2仿货' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'prod_property'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0未指定，1挂号，2平邮' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'ex_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否上架（0待审核，1销售中，2已下架，3回收站）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'prod_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'prod_title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'英文标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'title_en'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日文标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'title_jp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'预估重量（克）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'pre_weight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际重量（克）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'real_weight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品链接' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'prod_url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'以逗号分隔' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'prod_model'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'以逗号分隔' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'prod_size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'款号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'prod_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标签' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'prod_tag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'规格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'prod_spec'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片包地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'prod_picpath'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开发人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'prod_developer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已删除;1:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础数据-商品表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product_imgs', @level2type=N'COLUMN',@level2name=N'img_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product_imgs', @level2type=N'COLUMN',@level2name=N'prod_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按逗号分隔拆分成多行' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product_imgs', @level2type=N'COLUMN',@level2name=N'prod_model'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product_imgs', @level2type=N'COLUMN',@level2name=N'pic_url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product_imgs', @level2type=N'COLUMN',@level2name=N'pic_describe'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product_imgs', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product_imgs', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product_imgs', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product_imgs', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已删除;1:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product_imgs', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product_imgs', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product_imgs', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product_imgs', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础数据-商品图片列表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product_imgs'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_productclass', @level2type=N'COLUMN',@level2name=N'prod_class_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父类别ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_productclass', @level2type=N'COLUMN',@level2name=N'parent_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_productclass', @level2type=N'COLUMN',@level2name=N'class_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_productclass', @level2type=N'COLUMN',@level2name=N'class_icon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_productclass', @level2type=N'COLUMN',@level2name=N'class_sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_productclass', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_productclass', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_productclass', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_productclass', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已删除;1:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_productclass', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_productclass', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_productclass', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_productclass', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础数据-商品分类表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_productclass'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role', @level2type=N'COLUMN',@level2name=N'role_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role', @level2type=N'COLUMN',@level2name=N'org_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网点(公司)ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role', @level2type=N'COLUMN',@level2name=N'net_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role', @level2type=N'COLUMN',@level2name=N'role_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：停用；1：正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role', @level2type=N'COLUMN',@level2name=N'role_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.有效;0.已删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色菜单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_menu_rel', @level2type=N'COLUMN',@level2name=N'role_menu_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_menu_rel', @level2type=N'COLUMN',@level2name=N'role_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单权限ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_menu_rel', @level2type=N'COLUMN',@level2name=N'menu_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单分组ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_menu_rel', @level2type=N'COLUMN',@level2name=N'menu_group_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网点(公司)ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_menu_rel', @level2type=N'COLUMN',@level2name=N'net_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_menu_rel', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_menu_rel', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_menu_rel', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_menu_rel', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.有效;0.已删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_menu_rel', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_menu_rel', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_menu_rel', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色菜单关系表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_menu_rel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户与角色关系ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_user_rel', @level2type=N'COLUMN',@level2name=N'role_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_user_rel', @level2type=N'COLUMN',@level2name=N'role_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_user_rel', @level2type=N'COLUMN',@level2name=N'user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网点(公司)ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_user_rel', @level2type=N'COLUMN',@level2name=N'net_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_user_rel', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_user_rel', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_user_rel', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_user_rel', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.有效;0.已删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_user_rel', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_user_rel', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_user_rel', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户与角色绑定表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_role_user_rel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'shop_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'platform_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属国家id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'country_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'shop_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺帐号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'shop_account'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'取系统字典表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'settlm_currency'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台扣点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'platform_lrish'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1:可用;0:停用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'shop_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'shop_telephone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺邮编' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'shop_zipcode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'shop_address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已删除;1:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础数据-店铺表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'供应商id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_supplier', @level2type=N'COLUMN',@level2name=N'supp_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'供应商编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_supplier', @level2type=N'COLUMN',@level2name=N'supp_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购来源id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_supplier', @level2type=N'COLUMN',@level2name=N'purc_sourceid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购方式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_supplier', @level2type=N'COLUMN',@level2name=N'purc_mode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'供应商名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_supplier', @level2type=N'COLUMN',@level2name=N'supp_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'供应商网店地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_supplier', @level2type=N'COLUMN',@level2name=N'supp_url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购优先级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_supplier', @level2type=N'COLUMN',@level2name=N'purc_priority'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1:是;0:否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_supplier', @level2type=N'COLUMN',@level2name=N'is_grade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_supplier', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_supplier', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_supplier', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_supplier', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已删除;1:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_supplier', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_supplier', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_supplier', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_supplier', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础数据-供应商表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_supplier'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户与菜单关系ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_user_menu_rel', @level2type=N'COLUMN',@level2name=N'user_menu_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单权限ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_user_menu_rel', @level2type=N'COLUMN',@level2name=N'menu_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单分组ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_user_menu_rel', @level2type=N'COLUMN',@level2name=N'menu_group_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_user_menu_rel', @level2type=N'COLUMN',@level2name=N'user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色菜单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_user_menu_rel', @level2type=N'COLUMN',@level2name=N'role_menu_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网点(公司)ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_user_menu_rel', @level2type=N'COLUMN',@level2name=N'net_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_user_menu_rel', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_user_menu_rel', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_user_menu_rel', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_user_menu_rel', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.有效;0.已删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_user_menu_rel', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_user_menu_rel', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_user_menu_rel', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户与菜单关系表(可由各公司网点自行分配)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_user_menu_rel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'emp_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户工号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'emp_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'user_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'user_pwd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网点(公司)表主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'net_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工所属组织' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'org_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'加密种子' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'salt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'real_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'sex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'座机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'telphone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：账户不可用；1：账户可用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'user_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'job_id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'job_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1：领导；2：成员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'duty'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1：首要职务;2:领导的主管；3：成员的主管  可以多选' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'position'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人IP地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'op_ip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'open_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'停用时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'disable_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单点登录CODE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'token_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单点登录证书有效期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'token_expiry_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申通总部接口user_id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'sto_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.有效;0.已删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_users'
GO
