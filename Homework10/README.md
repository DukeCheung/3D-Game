### Unity 网络小游戏

#### 视频地址：http://www.iqiyi.com/w_19rymj390x.html

__操作指南__ ：按住WSAD或者四个方向键即可移动玩家，Space加速。

__程序说明__ ：

本次我是将之前的巡逻兵做成了网络小游戏，规则与之前相同，只要被巡逻兵追到就算结束。同时玩家为那个小人，躲避巡逻兵的追捕。

首先是将UI.cs, FirstSceneCtroller.cs, PatrolFatory.cs 文件绑定到player上，这些在之前的版本里是绑定在一个empty对象上的。之后再设置play的NetworkIdentity 和 NetworkTransform，后者用于客户机与服务端之间的动作同步。然后新建一个Empty对象，添加NetworkManager和NetworkManagerHUD，再将player预制放入Spawn插槽中，即可正常运行。

还要注意的是需要在UI脚本里添加：

```c#
if (!isLocalPlayer)
            return;
```

使只有本地程序才能处理输入。



做这个还是有很大问题的，因为要涉及到预制的实例化，所以要绑定很多cs脚本，这样一来player对象就不只是一个简单的玩家了，而是承载了整个程序的很大部分，调试很复杂。

还有个问题是之前我是给每个玩家都绑定了一个相机，不过这样一来客户机和主机的画面就不一致了，很迷的问题。