# Beetle.DT（分布式压力测试工具）
  基于.NET实现的分布式压力测试工具，用户可以根据需求编写相关的测试用例；通过工具的管理界面即可以把测试用例推送到服务中心，
  再根据实际压测的需求把测试用例分配到不同节点上运行。工具会根据测试的情况实时获取测试结果，测试完成后用户还可以查询具体
  的测试报告。节点采用进程隔离的方式运行测试用例，所以测试用例的运行都是相互独立。
# 整个工具结构
  工具分为三个主要部分，分别是：管理中心，节点和用户管理端。在部署了管理中心和节点后，用户就可以通过管理端进行测试用例管理，运行和监控的操作。
  ![image](https://github.com/IKende/Beetle.DT/blob/master/beetledt1.png)
  ![image](https://github.com/IKende/Beetle.DT/blob/master/beetledt2.png)
  ![image](https://github.com/IKende/Beetle.DT/blob/master/beetledt3.png)
## 配置管理中心

## 配置节点



