USE [master]
GO
/****** Object:  Database [NewLecheng]    Script Date: 2017/5/24 17:55:01 ******/
CREATE DATABASE [NewLecheng]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NewLecheng', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\NewLecheng.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'NewLecheng_log', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\NewLecheng_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [NewLecheng] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NewLecheng].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NewLecheng] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NewLecheng] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NewLecheng] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NewLecheng] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NewLecheng] SET ARITHABORT OFF 
GO
ALTER DATABASE [NewLecheng] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NewLecheng] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NewLecheng] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NewLecheng] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NewLecheng] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NewLecheng] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NewLecheng] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NewLecheng] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NewLecheng] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NewLecheng] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NewLecheng] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NewLecheng] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NewLecheng] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NewLecheng] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NewLecheng] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NewLecheng] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NewLecheng] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NewLecheng] SET RECOVERY FULL 
GO
ALTER DATABASE [NewLecheng] SET  MULTI_USER 
GO
ALTER DATABASE [NewLecheng] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NewLecheng] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NewLecheng] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NewLecheng] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [NewLecheng] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'NewLecheng', N'ON'
GO
USE [NewLecheng]
GO
/****** Object:  Table [dbo].[base_country]    Script Date: 2017/5/24 17:55:01 ******/
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
/****** Object:  Table [dbo].[base_employee]    Script Date: 2017/5/24 17:55:01 ******/
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
/****** Object:  Table [dbo].[base_exp_comp]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_exp_comp](
	[express_id] [int] IDENTITY(1,1) NOT NULL,
	[express_name] [nvarchar](100) NOT NULL,
	[express_descrip] [nvarchar](255) NULL,
	[country_id] [int] NULL,
	[express_status] [bit] NOT NULL DEFAULT ((1)),
	[create_time] [datetime] NOT NULL DEFAULT (getdate()),
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL DEFAULT (getdate()),
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL DEFAULT ((1)),
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BASE_EXP_COMP] PRIMARY KEY CLUSTERED 
(
	[express_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_iccard_scanin]    Script Date: 2017/5/24 17:55:01 ******/
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
/****** Object:  Table [dbo].[base_job]    Script Date: 2017/5/24 17:55:01 ******/
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
/****** Object:  Table [dbo].[base_location]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_location](
	[locat_id] [int] IDENTITY(1,1) NOT NULL,
	[wh_id] [bigint] NULL,
	[locat_code] [nvarchar](30) NULL,
	[locat_status] [bit] NOT NULL DEFAULT ((1)),
	[locat_type] [tinyint] NOT NULL DEFAULT ((2)),
	[create_time] [datetime] NOT NULL DEFAULT (getdate()),
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL DEFAULT (getdate()),
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL DEFAULT ((1)),
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BASE_LOCATION] PRIMARY KEY CLUSTERED 
(
	[locat_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_login_error]    Script Date: 2017/5/24 17:55:01 ******/
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
/****** Object:  Table [dbo].[base_login_log]    Script Date: 2017/5/24 17:55:01 ******/
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
/****** Object:  Table [dbo].[base_menu_groups]    Script Date: 2017/5/24 17:55:01 ******/
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
/****** Object:  Table [dbo].[base_menus]    Script Date: 2017/5/24 17:55:01 ******/
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
/****** Object:  Table [dbo].[base_org]    Script Date: 2017/5/24 17:55:01 ******/
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
/****** Object:  Table [dbo].[base_org_menu_rel]    Script Date: 2017/5/24 17:55:01 ******/
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
/****** Object:  Table [dbo].[base_platform]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_platform](
	[platform_id] [int] IDENTITY(1,1) NOT NULL,
	[platform_name] [nvarchar](100) NOT NULL,
	[create_time] [datetime] NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NULL,
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
/****** Object:  Table [dbo].[base_prod_code]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_prod_code](
	[code_id] [bigint] IDENTITY(1,1) NOT NULL,
	[prod_id] [bigint] NULL,
	[sku_code] [nvarchar](50) NULL,
	[jcode] [nvarchar](50) NULL,
	[isbn_code] [nvarchar](50) NULL,
	[bar_code] [nvarchar](50) NULL,
	[prod_model] [nvarchar](50) NULL,
	[prod_size] [nvarchar](50) NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BASE_PROD_CODE] PRIMARY KEY CLUSTERED 
(
	[code_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_prod_supp_rel]    Script Date: 2017/5/24 17:55:01 ******/
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
/****** Object:  Table [dbo].[base_product]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_product](
	[prod_id] [bigint] IDENTITY(10000,1) NOT NULL,
	[prod_class_id] [int] NULL,
	[brand_id] [int] NULL,
	[pic_url] [nvarchar](500) NOT NULL,
	[price_us] [decimal](8, 4) NULL,
	[price_cn] [decimal](8, 4) NULL,
	[price_jp] [decimal](8, 4) NULL,
	[prod_property] [int] NOT NULL,
	[ex_type] [int] NOT NULL,
	[prod_status] [int] NOT NULL,
	[prod_title] [nvarchar](200) NULL,
	[title_en] [nvarchar](500) NULL,
	[title_jp] [nvarchar](500) NULL,
	[pre_weight] [int] NULL,
	[real_weight] [int] NULL,
	[prod_style] [nvarchar](50) NULL,
	[prod_url] [nvarchar](500) NULL,
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
/****** Object:  Table [dbo].[base_product_imgs]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_product_imgs](
	[img_id] [bigint] IDENTITY(1,1) NOT NULL,
	[prod_id] [bigint] NULL,
	[code_id] [bigint] NULL,
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
/****** Object:  Table [dbo].[base_productclass]    Script Date: 2017/5/24 17:55:01 ******/
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
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NULL,
	[edit_user_id] [bigint] NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BASE_PRODUCTCLASS] PRIMARY KEY CLUSTERED 
(
	[prod_class_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_PSKU]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_PSKU](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ShopID] [int] NULL,
	[PSKU] [nvarchar](50) NULL,
	[SSKU] [nvarchar](50) NULL,
	[GSKU] [nvarchar](50) NULL,
 CONSTRAINT [PK_base_PSKU] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_role]    Script Date: 2017/5/24 17:55:01 ******/
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
/****** Object:  Table [dbo].[base_role_menu_rel]    Script Date: 2017/5/24 17:55:01 ******/
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
/****** Object:  Table [dbo].[base_role_user_rel]    Script Date: 2017/5/24 17:55:01 ******/
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
/****** Object:  Table [dbo].[base_shop]    Script Date: 2017/5/24 17:55:01 ******/
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
	[Csv_insert] [nvarchar](50) NULL,
	[LStablename] [nvarchar](50) NULL,
 CONSTRAINT [PK_BASE_SHOP] PRIMARY KEY CLUSTERED 
(
	[shop_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_supplier]    Script Date: 2017/5/24 17:55:01 ******/
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
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BASE_SUPPLIER] PRIMARY KEY CLUSTERED 
(
	[supp_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_user_menu_rel]    Script Date: 2017/5/24 17:55:01 ******/
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
/****** Object:  Table [dbo].[base_users]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[base_users](
	[user_id] [bigint] IDENTITY(1,1) NOT NULL,
	[emp_id] [bigint] NULL,
	[emp_no] [bigint] NULL,
	[user_name] [nvarchar](20) NOT NULL,
	[user_pwd] [varchar](50) NOT NULL,
	[net_no] [bigint] NULL,
	[org_id] [bigint] NULL,
	[salt] [varchar](10) NULL,
	[email] [nvarchar](50) NULL,
	[real_name] [varchar](50) NULL,
	[sex] [nvarchar](10) NULL,
	[telphone] [nvarchar](15) NULL,
	[mobile] [varchar](15) NULL,
	[user_status] [bit] NOT NULL,
	[job_id] [bigint] NULL,
	[duty] [tinyint] NULL,
	[position] [tinyint] NULL,
	[op_ip] [varchar](15) NULL,
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
/****** Object:  Table [dbo].[base_wh_stock]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_wh_stock](
	[stock_id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__base_wh_s__stock__27C3E46E]  DEFAULT (newid()),
	[net_no] [bigint] NULL,
	[consignor_id] [bigint] NULL,
	[supplier_id] [bigint] NULL,
	[area_id] [bigint] NULL,
	[location_id] [bigint] NULL,
	[wh_id] [bigint] NULL,
	[prod_id] [bigint] NULL,
	[code_id] [bigint] NOT NULL,
	[asset_class_id] [bigint] NULL,
	[stock_class] [tinyint] NULL CONSTRAINT [DF__base_wh_s__stock__28B808A7]  DEFAULT ((1)),
	[stock_code] [nvarchar](100) NULL CONSTRAINT [DF__base_wh_s__stock__29AC2CE0]  DEFAULT ('0'),
	[stock_barcode] [nvarchar](100) NULL,
	[service_life] [nvarchar](20) NULL,
	[purchase_price] [numeric](18, 0) NULL CONSTRAINT [DF__base_wh_s__purch__2AA05119]  DEFAULT ((0)),
	[reserve_qty] [numeric](18, 0) NULL CONSTRAINT [DF__base_wh_s__reser__2B947552]  DEFAULT ((0)),
	[stock_qty] [numeric](18, 0) NOT NULL CONSTRAINT [DF__base_wh_s__stock__2C88998B]  DEFAULT ((0)),
	[occupied_qty] [numeric](18, 0) NULL CONSTRAINT [DF__base_wh_s__occup__2D7CBDC4]  DEFAULT ((0)),
	[locking_qty] [numeric](18, 0) NULL CONSTRAINT [DF__base_wh_s__locki__2E70E1FD]  DEFAULT ((0)),
	[pallet_id] [bigint] NULL,
	[storage_time] [datetime] NULL,
	[retrieval_time] [datetime] NULL,
	[using_state] [bigint] NULL,
	[stock_status] [bit] NOT NULL CONSTRAINT [DF__base_wh_s__stock__2F650636]  DEFAULT ((1)),
	[create_time] [datetime] NOT NULL CONSTRAINT [DF__base_wh_s__creat__30592A6F]  DEFAULT (getdate()),
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL CONSTRAINT [DF__base_wh_s__edit___314D4EA8]  DEFAULT (getdate()),
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL CONSTRAINT [DF__base_wh_s__del_f__324172E1]  DEFAULT ((1)),
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BASE_WH_STOCK] PRIMARY KEY CLUSTERED 
(
	[stock_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_wh_stock_inout]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_wh_stock_inout](
	[oper_id] [bigint] IDENTITY(1,1) NOT NULL,
	[stock_id] [uniqueidentifier] NOT NULL,
	[user_name] [nvarchar](50) NOT NULL,
	[oper_type] [int] NOT NULL,
	[pre_count] [numeric](8, 4) NOT NULL,
	[oper_count] [numeric](8, 4) NOT NULL,
	[last_count] [numeric](8, 4) NOT NULL,
	[user_id] [bigint] NOT NULL,
	[add_time] [datetime] NOT NULL,
 CONSTRAINT [PK_BASE_WH_STOCK_INOUT] PRIMARY KEY CLUSTERED 
(
	[oper_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[base_wh_warehouse]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[base_wh_warehouse](
	[wh_id] [bigint] IDENTITY(1,1) NOT NULL,
	[net_no] [bigint] NULL,
	[wh_code] [nvarchar](30) NOT NULL,
	[wh_name] [nvarchar](50) NULL,
	[wh_address] [nvarchar](255) NULL,
	[wh_uplimit_qty] [numeric](8, 4) NULL,
	[wh_lowlimit_qty] [numeric](8, 4) NULL,
	[wh_alarm_qty] [numeric](8, 4) NULL,
	[wh_status] [bit] NULL,
	[wh_phone] [nvarchar](30) NULL,
	[wu_contacts] [nvarchar](30) NULL,
	[create_time] [datetime] NULL,
	[create_user_id] [bigint] NULL,
	[edit_time] [datetime] NULL,
	[edit_user_id] [bigint] NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BASE_WH_WAREHOUSE] PRIMARY KEY CLUSTERED 
(
	[wh_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[busi_custorder]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[busi_custorder](
	[order_id] [bigint] IDENTITY(1,1) NOT NULL,
	[platform_id] [int] NOT NULL,
	[shop_id] [bigint] NOT NULL,
	[custorder_code] [nvarchar](50) NULL,
	[order_code] [nvarchar](50) NOT NULL,
	[order_status] [int] NULL,
	[order_date] [datetime] NULL,
	[validity_day] [date] NULL,
	[latest_date] [date] NULL,
	[storage_type] [tinyint] NULL,
	[order_sumqty] [int] NULL,
	[order_summoney] [decimal](10, 2) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_busi_custorder] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[busi_custorder_detail]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[busi_custorder_detail](
	[detail_id] [bigint] IDENTITY(1,1) NOT NULL,
	[order_id] [bigint] NULL,
	[prod_id] [bigint] NOT NULL,
	[prod_name] [nvarchar](100) NULL,
	[code_id] [bigint] NOT NULL,
	[prod_num] [numeric](8, 4) NOT NULL,
	[prod_price] [numeric](8, 4) NULL,
	[pic_url] [nvarchar](200) NULL,
	[prod_weight] [numeric](8, 4) NULL,
	[detail_status] [tinyint] NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BUSI_CUSTORDER_DETAIL] PRIMARY KEY CLUSTERED 
(
	[detail_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[busi_printwork]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[busi_printwork](
	[p_Workid] [bigint] NOT NULL,
	[p_WorkType] [int] NULL,
	[data_1] [nvarchar](300) NULL,
	[data_2] [nvarchar](300) NULL,
	[data_3] [nvarchar](300) NULL,
	[data_4] [nvarchar](300) NULL,
	[data_5] [nvarchar](300) NULL,
	[data_6] [nvarchar](300) NULL,
	[data_7] [nvarchar](300) NULL,
	[data_8] [nvarchar](300) NULL,
	[data_9] [nvarchar](300) NULL,
	[data_10] [nvarchar](300) NULL,
	[p_idPoint] [int] NULL,
	[create_DateTime] [datetime] NULL,
	[create_UserID] [int] NULL,
	[p_Status] [int] NULL,
	[Print_DateTime] [datetime] NULL,
	[edit_DateTime] [datetime] NULL,
	[p_idActual] [int] NULL,
	[p_WorkRemarks] [nvarchar](300) NULL,
 CONSTRAINT [PK_BUSI_PRINTWORK] PRIMARY KEY CLUSTERED 
(
	[p_Workid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[busi_puch_freight]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[busi_puch_freight](
	[freight_id] [bigint] IDENTITY(1,1) NOT NULL,
	[purch_id] [bigint] NULL,
	[supp_id] [bigint] NULL,
	[freight] [numeric](8, 4) NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BUSI_PUCH_FREIGHT] PRIMARY KEY CLUSTERED 
(
	[freight_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[busi_purchase]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[busi_purchase](
	[purch_id] [bigint] IDENTITY(1,1) NOT NULL,
	[purch_code] [nvarchar](50) NULL,
	[purch_categories] [numeric](8, 4) NULL,
	[purch_numb] [numeric](8, 4) NULL,
	[sum_freight] [numeric](8, 4) NULL,
	[sum_money] [decimal](8, 4) NULL,
	[purch_sum] [numeric](8, 4) NULL,
	[purch_status] [int] NULL,
	[purch_remark] [nvarchar](255) NULL,
	[abnormal_remark] [nvarchar](255) NULL,
	[create_time] [datetime] NOT NULL CONSTRAINT [DF__busi_purc__creat__5090EFD7]  DEFAULT (getdate()),
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL CONSTRAINT [DF__busi_purc__edit___51851410]  DEFAULT (getdate()),
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL CONSTRAINT [DF__busi_purc__del_f__52793849]  DEFAULT ((1)),
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BUSI_PURCHASE] PRIMARY KEY CLUSTERED 
(
	[purch_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[busi_purchasedetail]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[busi_purchasedetail](
	[detail_id] [bigint] IDENTITY(1,1) NOT NULL,
	[cust_detail_id] [bigint] NULL,
	[shop_id] [bigint] NULL,
	[prod_id] [bigint] NULL,
	[code_id] [bigint] NULL,
	[purch_type] [tinyint] NOT NULL CONSTRAINT [DF__busi_purc__purch__6A85CC04]  DEFAULT ((1)),
	[purch_status] [int] NULL,
	[wh_id] [bigint] NULL,
	[purch_id] [bigint] NULL,
	[supp_id] [bigint] NULL,
	[purch_url] [nvarchar](500) NULL,
	[purch_rice] [decimal](8, 4) NULL,
	[purch_count] [decimal](8, 4) NULL,
	[lack_count] [decimal](8, 4) NULL,
	[err_count] [decimal](8, 4) NULL,
	[is_cancel] [bit] NOT NULL CONSTRAINT [DF__busi_purc__is_ca__6B79F03D]  DEFAULT ((0)),
	[cust_send_billcode] [nvarchar](40) NULL,
	[create_time] [datetime] NOT NULL CONSTRAINT [DF__busi_purc__creat__6C6E1476]  DEFAULT (getdate()),
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL CONSTRAINT [DF__busi_purc__edit___6D6238AF]  DEFAULT (getdate()),
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL CONSTRAINT [DF__busi_purc__del_f__6E565CE8]  DEFAULT ((1)),
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BUSI_PURCHASEDETAIL] PRIMARY KEY CLUSTERED 
(
	[detail_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[busi_sendorder]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[busi_sendorder](
	[order_id] [bigint] IDENTITY(1,1) NOT NULL,
	[tran_id] [bigint] NULL,
	[custorder_id] [bigint] NULL,
	[order_code] [nvarchar](50) NOT NULL,
	[express_id] [int] NOT NULL,
	[exp_code] [nvarchar](100) NULL,
	[prod_money] [numeric](8, 4) NOT NULL,
	[prod_num] [int] NOT NULL,
	[order_tatus] [int] NOT NULL,
	[is_pack] [bit] NULL,
	[pack_datetime] [datetime] NOT NULL,
	[send_datetime] [datetime] NULL,
	[is_export] [bit] NULL,
	[is_csv_export] [bit] NULL,
	[is_print] [bit] NULL,
	[print_time] [datetime] NULL,
	[receive_name] [nvarchar](50) NULL,
	[receive_address] [nvarchar](50) NULL,
	[receive_mobile] [nvarchar](50) NULL,
	[receive_phone] [nvarchar](50) NULL,
	[receive_zip] [nvarchar](50) NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BUSI_SENDORDER] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[busi_sendorder_detail]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[busi_sendorder_detail](
	[detail_id] [bigint] IDENTITY(1,1) NOT NULL,
	[order_id] [bigint] NULL,
	[prod_id] [bigint] NOT NULL,
	[prod_name] [nvarchar](100) NULL,
	[code_id] [bigint] NOT NULL,
	[prod_num] [numeric](8, 4) NOT NULL,
	[detail_status] [tinyint] NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BUSI_SENDORDER_DETAIL] PRIMARY KEY CLUSTERED 
(
	[detail_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[busi_transfer]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[busi_transfer](
	[tran_id] [bigint] IDENTITY(1,1) NOT NULL,
	[express_id] [int] NULL,
	[tran_code] [nvarchar](40) NULL,
	[tran_count] [numeric](8, 4) NULL,
	[tran_status] [tinyint] NULL,
	[express_code] [nvarchar](40) NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NOT NULL,
	[edit_user_id] [bigint] NOT NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BUSI_TRANSFER] PRIMARY KEY CLUSTERED 
(
	[tran_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[busi_workinfo]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[busi_workinfo](
	[work_id] [bigint] IDENTITY(1,1) NOT NULL,
	[custorder_id] [bigint] NOT NULL,
	[custorder_detail_id] [bigint] NOT NULL,
	[sendorder_detail_id] [bigint] NULL,
	[plat_id] [int] NULL,
	[shop_id] [bigint] NULL,
	[wh_id] [int] NULL,
	[area_id] [int] NULL,
	[locat_id] [int] NULL,
	[prod_code_id] [bigint] NOT NULL,
	[detail_source] [tinyint] NOT NULL CONSTRAINT [DF_busi_workinfo_detail_source]  DEFAULT ((2)),
	[is_work] [bit] NOT NULL CONSTRAINT [DF_busi_workinfo_is_work]  DEFAULT ((0)),
	[work_type] [tinyint] NOT NULL CONSTRAINT [DF_busi_workinfo_work_type]  DEFAULT ((2)),
	[work_time] [datetime] NULL,
	[create_time] [datetime] NOT NULL,
	[create_user_id] [bigint] NOT NULL,
	[edit_time] [datetime] NULL,
	[edit_user_id] [bigint] NULL,
	[del_flag] [bit] NOT NULL,
	[del_time] [datetime] NULL,
	[del_user_id] [bigint] NULL,
	[remark] [nvarchar](255) NULL,
 CONSTRAINT [PK_BUSI_WORKINFO] PRIMARY KEY CLUSTERED 
(
	[work_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LS_Order]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LS_Order](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[shop_id] [bigint] NULL,
	[Buyer] [nvarchar](30) NULL,
	[telephone] [nvarchar](30) NULL,
	[phone] [nvarchar](30) NULL,
	[zip] [nvarchar](30) NULL,
	[address] [nvarchar](300) NULL,
	[firstimport] [int] NULL,
	[ImportTime] [datetime] NULL,
	[Fee] [decimal](8, 4) NULL,
	[totilMoney] [decimal](8, 4) NULL,
	[SKU1] [nvarchar](50) NULL,
	[SKU2] [nvarchar](50) NULL,
	[Num] [int] NULL,
	[OrderNub] [nvarchar](100) NULL,
	[SysOrderNub] [nvarchar](100) NULL,
	[lasttime] [datetime] NULL,
	[beizhu] [nvarchar](200) NULL,
	[CustmerOrderTime] [datetime] NULL,
 CONSTRAINT [PK_LS_ORDERQ10] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LS_OrderDENA]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LS_OrderDENA](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[shop_id] [bigint] NULL,
	[Buyer] [nvarchar](30) NULL,
	[telephone] [nvarchar](30) NULL,
	[phone] [nvarchar](30) NULL,
	[zip] [nvarchar](30) NULL,
	[address] [nvarchar](300) NULL,
	[firstimport] [int] NULL,
	[ImportTime] [datetime] NULL,
	[Fee] [decimal](8, 4) NULL,
	[totilMoney] [decimal](8, 4) NULL,
	[SKU1] [nvarchar](50) NULL,
	[SKU2] [nvarchar](50) NULL,
	[Num] [numeric](18, 0) NULL,
	[OrderNub] [nvarchar](100) NULL,
	[SysOrderNub] [nvarchar](100) NULL,
	[lasttime] [datetime] NULL,
	[beizhu] [nvarchar](200) NULL,
	[CustmerOrderTime] [datetime] NULL,
 CONSTRAINT [PK_LS_ORDERNENA] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LS_OrderEbay]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LS_OrderEbay](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[shop_id] [bigint] NULL,
	[Buyer] [nvarchar](30) NULL,
	[telephone] [nvarchar](30) NULL,
	[phone] [nvarchar](30) NULL,
	[zip] [nvarchar](30) NULL,
	[address] [nvarchar](300) NULL,
	[firstimport] [int] NULL,
	[ImportTime] [datetime] NULL,
	[Fee] [decimal](8, 4) NULL,
	[totilMoney] [decimal](8, 4) NULL,
	[SKU1] [nvarchar](50) NULL,
	[SKU2] [nvarchar](50) NULL,
	[Num] [int] NULL,
	[OrderNub] [nvarchar](100) NULL,
	[SysOrderNub] [nvarchar](100) NULL,
	[beizhu] [nvarchar](200) NULL,
	[lasttime] [datetime] NULL,
	[CustmerOrderTime] [datetime] NULL,
 CONSTRAINT [PK_LS_ORDEREBAY] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LS_Orderletian]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LS_Orderletian](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[shop_id] [bigint] NULL,
	[Buyer] [nvarchar](30) NULL,
	[telephone] [nvarchar](30) NULL,
	[phone] [nvarchar](30) NULL,
	[zip] [nvarchar](30) NULL,
	[address] [nvarchar](300) NULL,
	[firstimport] [int] NULL,
	[ImportTime] [datetime] NULL,
	[Fee] [decimal](8, 4) NULL,
	[totilMoney] [decimal](8, 4) NULL,
	[SKU1] [nvarchar](50) NULL,
	[SKU2] [nvarchar](50) NULL,
	[Num] [numeric](18, 0) NULL,
	[OrderNub] [nvarchar](100) NULL,
	[SysOrderNub] [nvarchar](100) NULL,
	[lasttime] [datetime] NULL,
	[beizhu] [nvarchar](200) NULL,
	[CustmerOrderTime] [datetime] NULL,
 CONSTRAINT [PK_LS_ORDERLETIAN] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LS_Ordersumaitong]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LS_Ordersumaitong](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[shop_id] [bigint] NULL,
	[Buyer] [nvarchar](30) NULL,
	[telephone] [nvarchar](30) NULL,
	[phone] [nvarchar](30) NULL,
	[zip] [nvarchar](30) NULL,
	[address] [nvarchar](300) NULL,
	[firstimport] [int] NULL,
	[ImportTime] [datetime] NULL,
	[Fee] [decimal](8, 4) NULL,
	[totilMoney] [decimal](8, 4) NULL,
	[SKU1] [nvarchar](50) NULL,
	[SKU2] [nvarchar](50) NULL,
	[Num] [numeric](18, 0) NULL,
	[OrderNub] [nvarchar](100) NULL,
	[SysOrderNub] [nvarchar](100) NULL,
	[lasttime] [datetime] NULL,
	[beizhu] [nvarchar](200) NULL,
	[CustmerOrderTime] [datetime] NULL,
 CONSTRAINT [PK_LS_ORDERSUMAITONG] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LS_OrderWish]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LS_OrderWish](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[shop_id] [bigint] NULL,
	[Buyer] [nvarchar](30) NULL,
	[telephone] [nvarchar](30) NULL,
	[phone] [nvarchar](30) NULL,
	[zip] [nvarchar](30) NULL,
	[address] [nvarchar](300) NULL,
	[firstimport] [int] NULL,
	[ImportTime] [datetime] NULL,
	[Fee] [decimal](8, 8) NULL,
	[totilMoney] [decimal](8, 4) NULL,
	[SKU1] [nvarchar](50) NULL,
	[SKU2] [nvarchar](50) NULL,
	[Num] [numeric](18, 0) NULL,
	[OrderNub] [nvarchar](100) NULL,
	[SysOrderNub] [nvarchar](100) NULL,
	[lasttime] [datetime] NULL,
	[beizhu] [nvarchar](200) NULL,
	[CustmerOrderTime] [datetime] NULL,
 CONSTRAINT [PK_LS_ORDERWISH] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LS_Orderyahu]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LS_Orderyahu](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[shop_id] [bigint] NULL,
	[Buyer] [nvarchar](30) NULL,
	[telephone] [nvarchar](30) NULL,
	[phone] [nvarchar](30) NULL,
	[zip] [nvarchar](30) NULL,
	[address] [nvarchar](300) NULL,
	[firstimport] [int] NULL,
	[ImportTime] [datetime] NULL,
	[Fee] [decimal](8, 4) NULL,
	[totilMoney] [decimal](8, 4) NULL,
	[SKU1] [nvarchar](50) NULL,
	[SKU2] [nvarchar](50) NULL,
	[Num] [numeric](18, 0) NULL,
	[OrderNub] [nvarchar](100) NULL,
	[SysOrderNub] [nvarchar](100) NULL,
	[lasttime] [datetime] NULL,
	[beizhu] [nvarchar](200) NULL,
	[CustmerOrderTime] [datetime] NULL,
 CONSTRAINT [PK_LS_ORDERYAHU] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LS_Orderyamaxun]    Script Date: 2017/5/24 17:55:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LS_Orderyamaxun](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[shop_id] [bigint] NULL,
	[Buyer] [nvarchar](30) NULL,
	[telephone] [nvarchar](30) NULL,
	[phone] [nvarchar](30) NULL,
	[zip] [nvarchar](30) NULL,
	[address] [nvarchar](300) NULL,
	[firstimport] [int] NULL,
	[ImportTime] [datetime] NULL,
	[Fee] [decimal](8, 4) NULL,
	[totilMoney] [decimal](8, 4) NULL,
	[SKU1] [nvarchar](50) NULL,
	[SKU2] [nvarchar](50) NULL,
	[Num] [numeric](18, 0) NULL,
	[OrderNub] [nvarchar](100) NULL,
	[SysOrderNub] [nvarchar](100) NULL,
	[lasttime] [datetime] NULL,
	[beizhu] [nvarchar](200) NULL,
	[CustmerOrderTime] [datetime] NULL,
 CONSTRAINT [PK_LS_ORDERYAMAXUN] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [Index_1]    Script Date: 2017/5/24 17:55:01 ******/
CREATE NONCLUSTERED INDEX [Index_1] ON [dbo].[base_employee]
(
	[emp_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Index_1]    Script Date: 2017/5/24 17:55:01 ******/
CREATE NONCLUSTERED INDEX [Index_1] ON [dbo].[base_iccard_scanin]
(
	[ic_card_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Index_1]    Script Date: 2017/5/24 17:55:01 ******/
CREATE NONCLUSTERED INDEX [Index_1] ON [dbo].[base_menu_groups]
(
	[menu_group_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Index_1]    Script Date: 2017/5/24 17:55:01 ******/
CREATE NONCLUSTERED INDEX [Index_1] ON [dbo].[base_menus]
(
	[menu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Index_1]    Script Date: 2017/5/24 17:55:01 ******/
CREATE NONCLUSTERED INDEX [Index_1] ON [dbo].[base_org]
(
	[org_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Index_1]    Script Date: 2017/5/24 17:55:01 ******/
CREATE NONCLUSTERED INDEX [Index_1] ON [dbo].[base_org_menu_rel]
(
	[org_menu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Index_1]    Script Date: 2017/5/24 17:55:01 ******/
CREATE NONCLUSTERED INDEX [Index_1] ON [dbo].[base_role]
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Index_1]    Script Date: 2017/5/24 17:55:01 ******/
CREATE NONCLUSTERED INDEX [Index_1] ON [dbo].[base_role_menu_rel]
(
	[role_menu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Index_1]    Script Date: 2017/5/24 17:55:01 ******/
CREATE NONCLUSTERED INDEX [Index_1] ON [dbo].[base_role_user_rel]
(
	[role_user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Index_1]    Script Date: 2017/5/24 17:55:01 ******/
CREATE NONCLUSTERED INDEX [Index_1] ON [dbo].[base_user_menu_rel]
(
	[user_menu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [idx_tbl_users_nick_name]    Script Date: 2017/5/24 17:55:01 ******/
CREATE NONCLUSTERED INDEX [idx_tbl_users_nick_name] ON [dbo].[base_users]
(
	[user_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
GO
/****** Object:  Index [index_tbl_users_org_id]    Script Date: 2017/5/24 17:55:01 ******/
CREATE NONCLUSTERED INDEX [index_tbl_users_org_id] ON [dbo].[base_users]
(
	[org_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Index_1]    Script Date: 2017/5/24 17:55:01 ******/
CREATE NONCLUSTERED INDEX [Index_1] ON [dbo].[base_wh_stock]
(
	[stock_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Index_1]    Script Date: 2017/5/24 17:55:01 ******/
CREATE NONCLUSTERED INDEX [Index_1] ON [dbo].[base_wh_warehouse]
(
	[wh_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[base_wh_stock_inout] ADD  CONSTRAINT [DF__base_wh_s__stock__7ABC33CD]  DEFAULT (newid()) FOR [stock_id]
GO
ALTER TABLE [dbo].[busi_puch_freight] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[busi_puch_freight] ADD  DEFAULT (getdate()) FOR [edit_time]
GO
ALTER TABLE [dbo].[busi_puch_freight] ADD  DEFAULT ((1)) FOR [del_flag]
GO
ALTER TABLE [dbo].[busi_transfer] ADD  DEFAULT (getdate()) FOR [create_time]
GO
ALTER TABLE [dbo].[busi_transfer] ADD  DEFAULT (getdate()) FOR [edit_time]
GO
ALTER TABLE [dbo].[busi_transfer] ADD  DEFAULT ((1)) FOR [del_flag]
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物流公司id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_exp_comp', @level2type=N'COLUMN',@level2name=N'express_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物流公司名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_exp_comp', @level2type=N'COLUMN',@level2name=N'express_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物流公司描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_exp_comp', @level2type=N'COLUMN',@level2name=N'express_descrip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'国家表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_exp_comp', @level2type=N'COLUMN',@level2name=N'country_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.启用，0.停用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_exp_comp', @level2type=N'COLUMN',@level2name=N'express_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_exp_comp', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_exp_comp', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_exp_comp', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_exp_comp', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已删除;1:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_exp_comp', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_exp_comp', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_exp_comp', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_exp_comp', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础数据-物流公司表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_exp_comp'
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'库位ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_location', @level2type=N'COLUMN',@level2name=N'locat_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仓库ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_location', @level2type=N'COLUMN',@level2name=N'wh_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'库位号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_location', @level2type=N'COLUMN',@level2name=N'locat_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.可用;0.停用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_location', @level2type=N'COLUMN',@level2name=N'locat_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.临时库位;2.普通库位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_location', @level2type=N'COLUMN',@level2name=N'locat_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_location', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_location', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_location', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_location', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.有效;0.已删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_location', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_location', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_location', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_location', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础数据-仓库库位表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_location'
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'code_id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_code', @level2type=N'COLUMN',@level2name=N'code_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_code', @level2type=N'COLUMN',@level2name=N'prod_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'sku编码(条码)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_code', @level2type=N'COLUMN',@level2name=N'sku_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'jcode码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_code', @level2type=N'COLUMN',@level2name=N'jcode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ISBN码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_code', @level2type=N'COLUMN',@level2name=N'isbn_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品条码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_code', @level2type=N'COLUMN',@level2name=N'bar_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色/型号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_code', @level2type=N'COLUMN',@level2name=N'prod_model'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_code', @level2type=N'COLUMN',@level2name=N'prod_size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_code', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_code', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_code', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_code', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已删除;1:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_code', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_code', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_code', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_code', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础数据-商品编码表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_prod_code'
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题(商品名称)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'prod_title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'英文标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'title_en'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日文标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'title_jp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'预估重量（克）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'pre_weight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际重量（克）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'real_weight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'款号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'prod_style'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品链接' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product', @level2type=N'COLUMN',@level2name=N'prod_url'
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编码ID(商品编码表的ID)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_product_imgs', @level2type=N'COLUMN',@level2name=N'code_id'
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导入csv文件的SKU存储列' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_shop', @level2type=N'COLUMN',@level2name=N'Csv_insert'
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'库存ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'stock_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属网点(公司)ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'net_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'货主ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'consignor_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'供货方ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'supplier_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区域ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'area_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'货架ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'location_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仓库ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'wh_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'prod_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编码表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'code_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'资产类别ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'asset_class_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.固定资产;2.物料资产;3.商品' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'stock_class'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'库存分类为1或2时，该字段不能为空' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'stock_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'库存条码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'stock_barcode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用年限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'service_life'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进货价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'purchase_price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'库存预定数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'reserve_qty'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'库存数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'stock_qty'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'库存占用数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'occupied_qty'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'库存锁定数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'locking_qty'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'托盘ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'pallet_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入库时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'storage_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后出库时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'retrieval_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'取字典表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'using_state'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1:可用;0:停用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'stock_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.有效;0.已删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础数据-库存表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock_inout', @level2type=N'COLUMN',@level2name=N'oper_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'库存ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock_inout', @level2type=N'COLUMN',@level2name=N'stock_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock_inout', @level2type=N'COLUMN',@level2name=N'user_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.上架;2.下架 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock_inout', @level2type=N'COLUMN',@level2name=N'oper_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作前数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock_inout', @level2type=N'COLUMN',@level2name=N'pre_count'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock_inout', @level2type=N'COLUMN',@level2name=N'oper_count'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作后数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock_inout', @level2type=N'COLUMN',@level2name=N'last_count'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock_inout', @level2type=N'COLUMN',@level2name=N'user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock_inout', @level2type=N'COLUMN',@level2name=N'add_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务-仓库上下架操作表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_stock_inout'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仓库ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse', @level2type=N'COLUMN',@level2name=N'wh_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网点(公司)表主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse', @level2type=N'COLUMN',@level2name=N'net_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仓库编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse', @level2type=N'COLUMN',@level2name=N'wh_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仓库名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse', @level2type=N'COLUMN',@level2name=N'wh_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仓库地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse', @level2type=N'COLUMN',@level2name=N'wh_address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仓库上限数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse', @level2type=N'COLUMN',@level2name=N'wh_uplimit_qty'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仓库下限数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse', @level2type=N'COLUMN',@level2name=N'wh_lowlimit_qty'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仓库预警数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse', @level2type=N'COLUMN',@level2name=N'wh_alarm_qty'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.正常;0.停用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse', @level2type=N'COLUMN',@level2name=N'wh_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仓库联系电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse', @level2type=N'COLUMN',@level2name=N'wh_phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仓库联系人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse', @level2type=N'COLUMN',@level2name=N'wu_contacts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已删除;1:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础数据-仓库信息表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'base_wh_warehouse'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'order_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'platform_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'shop_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户系统订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'custorder_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统唯一' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'order_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.导入成功;10.已确认;11.已收货;20.已质检;30.已入库;40.已配货;50.已拣选;60.已包装;70.已发货;80.已转运;90.已再入库' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'order_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单据日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'order_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'预计发货日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'validity_day'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最迟发货日期(纳期)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'latest_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1:紧急;0:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'storage_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单总数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'order_sumqty'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单总金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'order_summoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已删除;1:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务-客户订单表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'明细id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder_detail', @level2type=N'COLUMN',@level2name=N'detail_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户订单表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder_detail', @level2type=N'COLUMN',@level2name=N'order_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder_detail', @level2type=N'COLUMN',@level2name=N'prod_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder_detail', @level2type=N'COLUMN',@level2name=N'prod_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编码表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder_detail', @level2type=N'COLUMN',@level2name=N'code_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder_detail', @level2type=N'COLUMN',@level2name=N'prod_num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品单价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder_detail', @level2type=N'COLUMN',@level2name=N'prod_price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品图片地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder_detail', @level2type=N'COLUMN',@level2name=N'pic_url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品重量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder_detail', @level2type=N'COLUMN',@level2name=N'prod_weight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0.初始;1.作业中;2.已完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder_detail', @level2type=N'COLUMN',@level2name=N'detail_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder_detail', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder_detail', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder_detail', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder_detail', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已删除;1:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder_detail', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder_detail', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder_detail', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder_detail', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务-客户订单明细表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_custorder_detail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'p_Workid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印任务类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'p_WorkType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'data_1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'data_2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'data_3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'data_4'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'data_5'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据6' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'data_6'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据7' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'data_7'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'data_8'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据9' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'data_9'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'data_10'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Base_Print表主键(0:不指定)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'p_idPoint'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日时' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'create_DateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'create_UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已打印 1:未打印' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'p_Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印日时' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'Print_DateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑日时' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'edit_DateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际打印客户端软件ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'p_idActual'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork', @level2type=N'COLUMN',@level2name=N'p_WorkRemarks'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务-打印任务表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_printwork'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运费ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_puch_freight', @level2type=N'COLUMN',@level2name=N'freight_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购单id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_puch_freight', @level2type=N'COLUMN',@level2name=N'purch_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'供应商表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_puch_freight', @level2type=N'COLUMN',@level2name=N'supp_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_puch_freight', @level2type=N'COLUMN',@level2name=N'freight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_puch_freight', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_puch_freight', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_puch_freight', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_puch_freight', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已删除;1:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_puch_freight', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_puch_freight', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_puch_freight', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_puch_freight', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务-采购单运费表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_puch_freight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购单id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchase', @level2type=N'COLUMN',@level2name=N'purch_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchase', @level2type=N'COLUMN',@level2name=N'purch_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购商品种类数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchase', @level2type=N'COLUMN',@level2name=N'purch_categories'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购总数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchase', @level2type=N'COLUMN',@level2name=N'purch_numb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运费总金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchase', @level2type=N'COLUMN',@level2name=N'sum_freight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'货物总金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchase', @level2type=N'COLUMN',@level2name=N'sum_money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合计总金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchase', @level2type=N'COLUMN',@level2name=N'purch_sum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.初始;2.已采购;3.已全部到货' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchase', @level2type=N'COLUMN',@level2name=N'purch_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购备注说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchase', @level2type=N'COLUMN',@level2name=N'purch_remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'异常处理备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchase', @level2type=N'COLUMN',@level2name=N'abnormal_remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchase', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchase', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchase', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchase', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已删除;1:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchase', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchase', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchase', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchase', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务-采购单表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'明细ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'detail_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户订单明细ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'cust_detail_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'shop_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'prod_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编码表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'code_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单采购1,库存采购2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'purch_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'待采购1,待填单号2,已采购3,已收货4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'purch_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仓库ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'wh_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'purch_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'供应商表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'supp_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购链接' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'purch_url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购单价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'purch_rice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'purch_count'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缺货数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'lack_count'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发错数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'err_count'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.是，0.否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'is_cancel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'供货商发货运单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'cust_send_billcode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已删除;1:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务-采购单详情表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_purchasedetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发货订单id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'order_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转运表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'tran_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户订单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'custorder_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发货编码(包裹号)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'order_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物流公司表id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'express_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'国外发货快递单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'exp_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'包裹总金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'prod_money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品总数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'prod_num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'40.已配货;50.已拣选;60.已包装;70.已发货;80.已转运;90.已再入库' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'order_tatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.是，0.否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'is_pack'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打包时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'pack_datetime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发货时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'send_datetime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(是否导出拣选)0未导出，1 已导出' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'is_export'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'快递单号是否已导回平台(1.是，0.否)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'is_csv_export'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否打印面单(1.是，0.否)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'is_print'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收件人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'receive_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收件人地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'receive_address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收件人手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'receive_mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收件人电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'receive_phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收件人邮编' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'receive_zip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已删除;1:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务-发货订单表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'明细id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder_detail', @level2type=N'COLUMN',@level2name=N'detail_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发货订单id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder_detail', @level2type=N'COLUMN',@level2name=N'order_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder_detail', @level2type=N'COLUMN',@level2name=N'prod_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder_detail', @level2type=N'COLUMN',@level2name=N'prod_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编码ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder_detail', @level2type=N'COLUMN',@level2name=N'code_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品发货数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder_detail', @level2type=N'COLUMN',@level2name=N'prod_num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0.初始;1.作业中;2.已完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder_detail', @level2type=N'COLUMN',@level2name=N'detail_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder_detail', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder_detail', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder_detail', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder_detail', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已删除;1:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder_detail', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder_detail', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder_detail', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder_detail', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务-发货订单明细表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_sendorder_detail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转运ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_transfer', @level2type=N'COLUMN',@level2name=N'tran_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物流公司id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_transfer', @level2type=N'COLUMN',@level2name=N'express_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统转运单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_transfer', @level2type=N'COLUMN',@level2name=N'tran_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转运单发货订单数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_transfer', @level2type=N'COLUMN',@level2name=N'tran_count'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.未转运，2已转运' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_transfer', @level2type=N'COLUMN',@level2name=N'tran_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物流单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_transfer', @level2type=N'COLUMN',@level2name=N'express_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_transfer', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_transfer', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_transfer', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_transfer', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:已删除;1:正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_transfer', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_transfer', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_transfer', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_transfer', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务-转运表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_transfer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作业ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'work_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户订单表ID(方便配货使用)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'custorder_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户订单明细id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'custorder_detail_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发货单明细ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'sendorder_detail_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'plat_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'shop_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仓库' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'wh_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区域' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'area_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'货位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'locat_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编码表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'prod_code_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'货物使用来源(使用库存1，正常采购2)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'detail_source'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否已配(0,未配 1,已配)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'is_work'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配货方式(1,网页配货，2手持配货，3网页批量，4库存配货)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'work_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配货时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'work_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'create_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'create_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'edit_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'edit_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1.有效;0.已删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'del_flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'del_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'del_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务-配货作业表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'busi_workinfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'OrderID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Order', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Order', @level2type=N'COLUMN',@level2name=N'shop_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Order', @level2type=N'COLUMN',@level2name=N'Buyer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Order', @level2type=N'COLUMN',@level2name=N'telephone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Order', @level2type=N'COLUMN',@level2name=N'phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户邮编' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Order', @level2type=N'COLUMN',@level2name=N'zip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Order', @level2type=N'COLUMN',@level2name=N'address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否第一次导入' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Order', @level2type=N'COLUMN',@level2name=N'firstimport'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Order', @level2type=N'COLUMN',@level2name=N'ImportTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Order', @level2type=N'COLUMN',@level2name=N'Fee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品总价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Order', @level2type=N'COLUMN',@level2name=N'totilMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品SKU信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Order', @level2type=N'COLUMN',@level2name=N'SKU1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品SKU信息2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Order', @level2type=N'COLUMN',@level2name=N'SKU2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Order', @level2type=N'COLUMN',@level2name=N'Num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Order', @level2type=N'COLUMN',@level2name=N'OrderNub'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'乐诚订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Order', @level2type=N'COLUMN',@level2name=N'SysOrderNub'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'纳期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Order', @level2type=N'COLUMN',@level2name=N'lasttime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Order', @level2type=N'COLUMN',@level2name=N'beizhu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务-Q10平台订单临时表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Order'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'OrderID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderDENA', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderDENA', @level2type=N'COLUMN',@level2name=N'shop_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderDENA', @level2type=N'COLUMN',@level2name=N'Buyer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderDENA', @level2type=N'COLUMN',@level2name=N'telephone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderDENA', @level2type=N'COLUMN',@level2name=N'phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户邮编' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderDENA', @level2type=N'COLUMN',@level2name=N'zip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderDENA', @level2type=N'COLUMN',@level2name=N'address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否第一次导入' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderDENA', @level2type=N'COLUMN',@level2name=N'firstimport'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderDENA', @level2type=N'COLUMN',@level2name=N'ImportTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderDENA', @level2type=N'COLUMN',@level2name=N'Fee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品总价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderDENA', @level2type=N'COLUMN',@level2name=N'totilMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品SKU信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderDENA', @level2type=N'COLUMN',@level2name=N'SKU1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品SKU信息2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderDENA', @level2type=N'COLUMN',@level2name=N'SKU2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderDENA', @level2type=N'COLUMN',@level2name=N'Num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderDENA', @level2type=N'COLUMN',@level2name=N'OrderNub'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'乐诚订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderDENA', @level2type=N'COLUMN',@level2name=N'SysOrderNub'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'纳期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderDENA', @level2type=N'COLUMN',@level2name=N'lasttime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderDENA', @level2type=N'COLUMN',@level2name=N'beizhu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务-NENA平台订单临时表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderDENA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'OrderID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderEbay', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderEbay', @level2type=N'COLUMN',@level2name=N'shop_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderEbay', @level2type=N'COLUMN',@level2name=N'Buyer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderEbay', @level2type=N'COLUMN',@level2name=N'telephone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderEbay', @level2type=N'COLUMN',@level2name=N'phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户邮编' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderEbay', @level2type=N'COLUMN',@level2name=N'zip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderEbay', @level2type=N'COLUMN',@level2name=N'address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否第一次导入' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderEbay', @level2type=N'COLUMN',@level2name=N'firstimport'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderEbay', @level2type=N'COLUMN',@level2name=N'ImportTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderEbay', @level2type=N'COLUMN',@level2name=N'Fee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品总价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderEbay', @level2type=N'COLUMN',@level2name=N'totilMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品SKU信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderEbay', @level2type=N'COLUMN',@level2name=N'SKU1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品SKU信息2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderEbay', @level2type=N'COLUMN',@level2name=N'SKU2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderEbay', @level2type=N'COLUMN',@level2name=N'Num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderEbay', @level2type=N'COLUMN',@level2name=N'OrderNub'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'乐诚订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderEbay', @level2type=N'COLUMN',@level2name=N'SysOrderNub'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderEbay', @level2type=N'COLUMN',@level2name=N'beizhu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'纳期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderEbay', @level2type=N'COLUMN',@level2name=N'lasttime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务-Ebay平台订单临时表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderEbay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'OrderID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderletian', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderletian', @level2type=N'COLUMN',@level2name=N'shop_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderletian', @level2type=N'COLUMN',@level2name=N'Buyer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderletian', @level2type=N'COLUMN',@level2name=N'telephone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderletian', @level2type=N'COLUMN',@level2name=N'phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户邮编' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderletian', @level2type=N'COLUMN',@level2name=N'zip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderletian', @level2type=N'COLUMN',@level2name=N'address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否第一次导入' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderletian', @level2type=N'COLUMN',@level2name=N'firstimport'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderletian', @level2type=N'COLUMN',@level2name=N'ImportTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderletian', @level2type=N'COLUMN',@level2name=N'Fee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品总价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderletian', @level2type=N'COLUMN',@level2name=N'totilMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品SKU信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderletian', @level2type=N'COLUMN',@level2name=N'SKU1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品SKU信息2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderletian', @level2type=N'COLUMN',@level2name=N'SKU2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderletian', @level2type=N'COLUMN',@level2name=N'Num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderletian', @level2type=N'COLUMN',@level2name=N'OrderNub'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'乐诚订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderletian', @level2type=N'COLUMN',@level2name=N'SysOrderNub'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'纳期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderletian', @level2type=N'COLUMN',@level2name=N'lasttime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderletian', @level2type=N'COLUMN',@level2name=N'beizhu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务-letian平台订单临时表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderletian'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'OrderID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Ordersumaitong', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Ordersumaitong', @level2type=N'COLUMN',@level2name=N'shop_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Ordersumaitong', @level2type=N'COLUMN',@level2name=N'Buyer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Ordersumaitong', @level2type=N'COLUMN',@level2name=N'telephone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Ordersumaitong', @level2type=N'COLUMN',@level2name=N'phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户邮编' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Ordersumaitong', @level2type=N'COLUMN',@level2name=N'zip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Ordersumaitong', @level2type=N'COLUMN',@level2name=N'address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否第一次导入' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Ordersumaitong', @level2type=N'COLUMN',@level2name=N'firstimport'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Ordersumaitong', @level2type=N'COLUMN',@level2name=N'ImportTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Ordersumaitong', @level2type=N'COLUMN',@level2name=N'Fee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品总价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Ordersumaitong', @level2type=N'COLUMN',@level2name=N'totilMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品SKU信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Ordersumaitong', @level2type=N'COLUMN',@level2name=N'SKU1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品SKU信息2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Ordersumaitong', @level2type=N'COLUMN',@level2name=N'SKU2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Ordersumaitong', @level2type=N'COLUMN',@level2name=N'Num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Ordersumaitong', @level2type=N'COLUMN',@level2name=N'OrderNub'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'乐诚订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Ordersumaitong', @level2type=N'COLUMN',@level2name=N'SysOrderNub'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'纳期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Ordersumaitong', @level2type=N'COLUMN',@level2name=N'lasttime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Ordersumaitong', @level2type=N'COLUMN',@level2name=N'beizhu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务-sumaitong平台订单临时表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Ordersumaitong'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'OrderID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderWish', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderWish', @level2type=N'COLUMN',@level2name=N'shop_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderWish', @level2type=N'COLUMN',@level2name=N'Buyer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderWish', @level2type=N'COLUMN',@level2name=N'telephone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderWish', @level2type=N'COLUMN',@level2name=N'phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户邮编' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderWish', @level2type=N'COLUMN',@level2name=N'zip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderWish', @level2type=N'COLUMN',@level2name=N'address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否第一次导入' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderWish', @level2type=N'COLUMN',@level2name=N'firstimport'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderWish', @level2type=N'COLUMN',@level2name=N'ImportTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderWish', @level2type=N'COLUMN',@level2name=N'Fee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品总价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderWish', @level2type=N'COLUMN',@level2name=N'totilMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品SKU信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderWish', @level2type=N'COLUMN',@level2name=N'SKU1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品SKU信息2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderWish', @level2type=N'COLUMN',@level2name=N'SKU2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderWish', @level2type=N'COLUMN',@level2name=N'Num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderWish', @level2type=N'COLUMN',@level2name=N'OrderNub'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'乐诚订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderWish', @level2type=N'COLUMN',@level2name=N'SysOrderNub'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'纳期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderWish', @level2type=N'COLUMN',@level2name=N'lasttime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderWish', @level2type=N'COLUMN',@level2name=N'beizhu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务-Wish平台订单临时表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_OrderWish'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'OrderID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyahu', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyahu', @level2type=N'COLUMN',@level2name=N'shop_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyahu', @level2type=N'COLUMN',@level2name=N'Buyer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyahu', @level2type=N'COLUMN',@level2name=N'telephone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyahu', @level2type=N'COLUMN',@level2name=N'phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户邮编' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyahu', @level2type=N'COLUMN',@level2name=N'zip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyahu', @level2type=N'COLUMN',@level2name=N'address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否第一次导入' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyahu', @level2type=N'COLUMN',@level2name=N'firstimport'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyahu', @level2type=N'COLUMN',@level2name=N'ImportTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyahu', @level2type=N'COLUMN',@level2name=N'Fee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品总价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyahu', @level2type=N'COLUMN',@level2name=N'totilMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品SKU信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyahu', @level2type=N'COLUMN',@level2name=N'SKU1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品SKU信息2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyahu', @level2type=N'COLUMN',@level2name=N'SKU2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyahu', @level2type=N'COLUMN',@level2name=N'Num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyahu', @level2type=N'COLUMN',@level2name=N'OrderNub'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'乐诚订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyahu', @level2type=N'COLUMN',@level2name=N'SysOrderNub'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'纳期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyahu', @level2type=N'COLUMN',@level2name=N'lasttime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyahu', @level2type=N'COLUMN',@level2name=N'beizhu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务-yahu平台订单临时表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyahu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'OrderID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyamaxun', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyamaxun', @level2type=N'COLUMN',@level2name=N'shop_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyamaxun', @level2type=N'COLUMN',@level2name=N'Buyer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyamaxun', @level2type=N'COLUMN',@level2name=N'telephone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyamaxun', @level2type=N'COLUMN',@level2name=N'phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户邮编' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyamaxun', @level2type=N'COLUMN',@level2name=N'zip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyamaxun', @level2type=N'COLUMN',@level2name=N'address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否第一次导入' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyamaxun', @level2type=N'COLUMN',@level2name=N'firstimport'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyamaxun', @level2type=N'COLUMN',@level2name=N'ImportTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyamaxun', @level2type=N'COLUMN',@level2name=N'Fee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品总价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyamaxun', @level2type=N'COLUMN',@level2name=N'totilMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品SKU信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyamaxun', @level2type=N'COLUMN',@level2name=N'SKU1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品SKU信息2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyamaxun', @level2type=N'COLUMN',@level2name=N'SKU2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyamaxun', @level2type=N'COLUMN',@level2name=N'Num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyamaxun', @level2type=N'COLUMN',@level2name=N'OrderNub'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'乐诚订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyamaxun', @level2type=N'COLUMN',@level2name=N'SysOrderNub'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'纳期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyamaxun', @level2type=N'COLUMN',@level2name=N'lasttime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyamaxun', @level2type=N'COLUMN',@level2name=N'beizhu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务-yamaxun平台订单临时表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LS_Orderyamaxun'
GO
USE [master]
GO
ALTER DATABASE [NewLecheng] SET  READ_WRITE 
GO
