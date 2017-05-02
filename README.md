# Spider

## 简介

自定义**爬虫**工具。目前用于网络小说的爬取。

## 目标

* 可方便扩展
* 适应页面布局变化

## 项目简要说明

* `D.Util.x` 逐步积累的一些开发所必需的基础工具组件，比如 `log`、`config`等。在 `spider` 中逐步完善，当到达一定程度之后就会分离成为一个单独项目。

    * 日志：`ILogWriter` 、`ILoggerFactory` 、`ILogger` 、`ILogContext`；
    * 配置：`IConfig` 、`IConfigItem` 。

* `D.Spider.x` 基本的爬虫流程。
* `D.NovelCrawl.x` 小说爬取相关。

## 计划

* `v1.x` 

1. 可以完成的用于网络小说的爬取；
2. 稳定长时间运行。

* `v2.x`

1. 插件式架构，方便爬虫业务扩展