using UnityEngine;
using ProFramework;

namespace ProFrameworkTest
{
    public class MathTest : MonoBehaviour
    {
        void Start()
        {
            TestDeg2Rad();
            TestRad2Deg();
            TestGetObjDistanceXZ();
            TestCheckObjDistanceXZ();
            TestGetObjDistanceXY();
            TestCheckObjDistanceXY();
            TestGetObjDistanceYZ();
            TestCheckObjDistanceYZ();
            TestIsWorldPosOutScreen();
            TestIsInSectorRangeXZ();
            TestRayCast();
            TestOverlapBox();
            TestOverlapSphere();
        }

        void TestDeg2Rad()
        {
            float deg = 45f;
            float rad = ProMathTool.Deg2Rad(deg);
            ProLog.LogDebug("Deg2Rad 测试: " + deg + " 度 = " + rad + " 弧度");
        }

        void TestRad2Deg()
        {
            float rad = Mathf.PI / 4f;
            float deg = ProMathTool.Rad2Deg(rad);
            ProLog.LogDebug("Rad2Deg 测试: " + rad + " 弧度 = " + deg + " 度");
        }

        void TestGetObjDistanceXZ()
        {
            Vector3 srcPos = new Vector3(0, 0, 0);
            Vector3 targetPos = new Vector3(3, 0, 4);
            float distance = ProMathTool.GetObjDistanceXZ(srcPos, targetPos);
            ProLog.LogDebug("GetObjDistanceXZ 测试: " + srcPos + " 和 " + targetPos + " 之间的距离 = " + distance);
        }

        void TestCheckObjDistanceXZ()
        {
            Vector3 srcPos = new Vector3(0, 0, 0);
            Vector3 targetPos = new Vector3(3, 0, 4);
            float distance = 5f;
            bool result = ProMathTool.CheckObjDistanceXZ(srcPos, targetPos, distance);
            ProLog.LogDebug("CheckObjDistanceXZ 测试: " + srcPos + " 和 " + targetPos + " 之间的距离 <= " + distance + " : " +
                            result);
        }

        void TestGetObjDistanceXY()
        {
            Vector3 srcPos = new Vector3(1, 2, 0);
            Vector3 targetPos = new Vector3(4, 6, 0);
            float distance = ProMathTool.GetObjDistanceXY(srcPos, targetPos);
            ProLog.LogDebug("GetObjDistanceXY 测试: " + srcPos + " 和 " + targetPos + " 之间的距离 = " + distance);
        }

        void TestCheckObjDistanceXY()
        {
            Vector3 srcPos = new Vector3(1, 2, 0);
            Vector3 targetPos = new Vector3(4, 6, 0);
            float distance = 5f;
            bool result = ProMathTool.CheckObjDistanceXY(srcPos, targetPos, distance);
            ProLog.LogDebug("CheckObjDistanceXY 测试: " + srcPos + " 和 " + targetPos + " 之间的距离 <= " + distance + " : " +
                            result);
        }

        void TestGetObjDistanceYZ()
        {
            Vector3 srcPos = new Vector3(0, 3, 2);
            Vector3 targetPos = new Vector3(0, 5, 6);
            float distance = ProMathTool.GetObjDistanceYZ(srcPos, targetPos);
            ProLog.LogDebug("GetObjDistanceYZ 测试: " + srcPos + " 和 " + targetPos + " 之间的距离 = " + distance);
        }

        void TestCheckObjDistanceYZ()
        {
            Vector3 srcPos = new Vector3(0, 3, 2);
            Vector3 targetPos = new Vector3(0, 5, 6);
            float distance = 5f;
            bool result = ProMathTool.CheckObjDistanceYZ(srcPos, targetPos, distance);
            ProLog.LogDebug("CheckObjDistanceYZ 测试: " + srcPos + " 和 " + targetPos + " 之间的距离 <= " + distance + " : " +
                            result);
        }

        void TestIsWorldPosOutScreen()
        {
            Vector3 pos = new Vector3(100, 100, 0);
            bool result = ProMathTool.IsWorldPosOutScreen(pos);
            ProLog.LogDebug("IsWorldPosOutScreen 测试: 位置 " + pos + " 是否在屏幕外: " + result);
        }

        void TestIsInSectorRangeXZ()
        {
            Vector3 pos = new Vector3(0, 0, 5);
            Vector3 forward = Vector3.forward;
            Vector3 targetPos = new Vector3(3, 0, 3);
            float radius = 5f;
            float angle = 90f;
            bool result = ProMathTool.IsInSectorRangeXZ(pos, forward, targetPos, radius, angle);
            ProLog.LogDebug("IsInSectorRangeXZ 测试: 位置 " + targetPos + " 是否在扇形范围内: " + result);
        }
        
        
        void TestRayCast()
        {
            Ray ray = new Ray(Vector3.zero, Vector3.forward);
            float maxDistance = 10f;
            int layerMask = LayerMask.GetMask("Default");
            ProMathTool.RayCast<Collider>(ray, (arg0) =>
            {
                ProLog.LogDebug("RayCast 测试: 射线碰撞到了物体 " + arg0.gameObject.name);
            }, maxDistance, layerMask);
        }

        void TestOverlapBox()
        {
            Vector3 center = Vector3.zero;
            Quaternion rotation = Quaternion.identity;
            Vector3 halfExtents = new Vector3(1, 1, 1);
            int layerMask = LayerMask.GetMask("Default");
            ProMathTool.OverlapBox<Collider>(center, rotation, halfExtents, layerMask, (arg0) =>
            {
                ProLog.LogDebug("OverlapBox 测试: 盒形范围检测到了物体 " + arg0.gameObject.name);
            });
        }

        void TestOverlapSphere()
        {
            Vector3 center = Vector3.zero;
            float radius = 1f;
            int layerMask = LayerMask.GetMask("Default");
            ProMathTool.OverlapSphere<Collider>(center, radius, layerMask, (arg0) =>
            {
                ProLog.LogDebug("OverlapSphere 测试: 球形范围检测到了物体 " + arg0.gameObject.name);
            });
        }
    }
}