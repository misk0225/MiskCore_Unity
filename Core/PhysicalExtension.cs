using MiskCore.Condition;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiskCore
{
    public static class PhysicalExtension
    {
        /// <summary>
        /// 發送一條射線，會由近到遠的回傳擊中的目標
        /// </summary>
        public static RaycastHit[] RaycastAllSorted(Ray ray, float maxDistance = Mathf.Infinity, int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
        {
            return SortRaycastResult(Physics.RaycastAll(ray, maxDistance, layerMask, queryTriggerInteraction));
        }
        public static RaycastHit[] RaycastAllSorted(Ray ray)
        {
            return SortRaycastResult(Physics.RaycastAll(ray));
        }
        public static RaycastHit[] RaycastAllSorted(Ray ray, float maxDistance)
        {
            return SortRaycastResult(Physics.RaycastAll(ray, maxDistance));
        }
        public static RaycastHit[] RaycastAllSorted(Ray ray, float maxDistance, int layerMask)
        {
            return SortRaycastResult(Physics.RaycastAll(ray, maxDistance, layerMask));
        }
        public static RaycastHit[] RaycastAllSorted(Vector3 origin, Vector3 direction)
        {
            return SortRaycastResult(Physics.RaycastAll(origin, direction));
        }
        public static RaycastHit[] RaycastAllSorted(Vector3 origin, Vector3 direction, float maxDistance)
        {
            return SortRaycastResult(Physics.RaycastAll(origin, direction, maxDistance));
        }
        public static RaycastHit[] RaycastAllSorted(Vector3 origin, Vector3 direction, float maxDistance, int layerMask)
        {
            return SortRaycastResult(Physics.RaycastAll(origin, direction, maxDistance, layerMask));
        }
        public static RaycastHit[] RaycastAllSorted(Vector3 origin, Vector3 direction, float maxDistance = Mathf.Infinity, int layerMask = Physics.DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
        {
            return SortRaycastResult(Physics.RaycastAll(origin, direction, maxDistance, layerMask, queryTriggerInteraction));
        }

        /// <summary>
        /// 發送一條射線，可以依照條件過濾目標
        /// </summary>
        public static bool Raycast(Ray ray, ICondition<RaycastHit> condition, out RaycastHit hitInfo, QueryTriggerInteraction triggerMode = QueryTriggerInteraction.UseGlobal)
        {
            return Raycast(ray, condition, out hitInfo, Mathf.Infinity, Physics.DefaultRaycastLayers, triggerMode);
        }
        public static bool Raycast(Ray ray, ICondition<RaycastHit> condition, out RaycastHit hitInfo, float maxDistance, QueryTriggerInteraction triggerMode = QueryTriggerInteraction.UseGlobal)
        {
            return Raycast(ray, condition, out hitInfo, maxDistance, Physics.DefaultRaycastLayers, triggerMode);
        }
        public static bool Raycast(Ray ray, ICondition<RaycastHit> condition, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction triggerMode = QueryTriggerInteraction.UseGlobal)
        {
            RaycastHit[] hits = RaycastAllSorted(ray, maxDistance, layerMask, triggerMode);
            return TryGetFilteredHitBySorted(hits, out hitInfo, condition);
        }
        public static bool Raycast(Vector3 origin, Vector3 direction, ICondition<RaycastHit> condition, out RaycastHit hitInfo, QueryTriggerInteraction triggerMode = QueryTriggerInteraction.UseGlobal)
        {
            return Raycast(origin, direction, condition, out hitInfo, Mathf.Infinity, Physics.DefaultRaycastLayers, triggerMode);
        }
        public static bool Raycast(Vector3 origin, Vector3 direction, ICondition<RaycastHit> condition, out RaycastHit hitInfo, float maxDistance, QueryTriggerInteraction triggerMode = QueryTriggerInteraction.UseGlobal)
        {
            return Raycast(origin, direction, condition, out hitInfo, maxDistance, Physics.DefaultRaycastLayers, triggerMode);
        }
        public static bool Raycast(Vector3 origin, Vector3 direction, ICondition<RaycastHit> condition, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction triggerMode = QueryTriggerInteraction.UseGlobal)
        {
            RaycastHit[] hits = RaycastAllSorted(origin, direction, maxDistance, layerMask, triggerMode);
            return TryGetFilteredHitBySorted(hits, out hitInfo, condition);
        }


        /// <summary>
        /// 對 RaycastAll 結果進行排序與條件過濾，回傳第一個符合條件的命中
        /// </summary>
        private static bool TryGetFilteredHitBySorted(RaycastHit[] hits, out RaycastHit hitInfo, ICondition<RaycastHit> condition)
        {
            hitInfo = new RaycastHit();

            if (hits.Length == 0)
                return false;

            // 過濾條件
            foreach (var hit in hits)
            {
                if (condition == null || condition.Do(hit))
                {
                    hitInfo = hit;
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// 將命中結果依照距離排序（由近到遠）
        /// </summary>
        private static RaycastHit[] SortRaycastResult(RaycastHit[] res)
        {
            if (res.Length > 1)
                Array.Sort(res, (a, b) => a.distance.CompareTo(b.distance));

            return res;
        }
    }
}

