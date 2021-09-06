# DesktopSprite
## 介绍
这是一个使用unity编写的桌面精灵

## 操作
左shift + 鼠标左键拖拽：移动精灵
左shift + 鼠标滚轮滚动：缩放精灵
左alt + 鼠标左/右键：切换表情
左alt + 鼠标滚轮：切换姿势

## 可能问题
1. 桌面不透明
* 解决办法：打开unity Edit/Project Settings/Player菜单栏，取消勾选Use DXGI Flip Model xxx一行。为什么可以暂时不清楚。
2. 无法聚焦到桌面精灵上或不显示桌面精灵
* 解决办法：
创建一个新的层Player，将人物放到该层，并使摄像头只渲染该层。具体如下：
在unity 的Hierachy工作区选中unitychan_dynamic，在Inspector工作区的Layer选项中，
使用Add Layer功能，添加一个名为Player的层，并将unitychan_dynamic置于此层中。
在Hierachy工作区选中摄像头，在Inspector工作区将其Culling Mask选项设为Player层。
