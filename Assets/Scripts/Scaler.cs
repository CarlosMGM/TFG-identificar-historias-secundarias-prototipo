using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class Scaler
    {
        Vector2 _refRes;
        Vector2 _currRes;

        float _unityUds;

        public Scaler(Vector2 res, Vector2 refRes, int camSize)
        {
            _currRes = res;
            _refRes = refRes;

            _unityUds = res.y / (2 * camSize);
        } // Constructor

        public Vector3 ScaleToFitScreen(Vector3 sizeInUnits, Vector3 scale)
        {
            Vector3 temp = sizeInUnits;

            temp.x *= _unityUds;
            temp.y *= _unityUds;

            temp.x = _currRes.x;

            temp.y = (temp.x * sizeInUnits.y) / sizeInUnits.x;

            // Reconvert to Unity units
            temp.x = temp.x / _unityUds;
            temp.y = temp.y / _unityUds;

            // New scale to apply
            return resizeObjectScale(sizeInUnits, temp, scale);
        } // ScaleToFitScreen

        public Vector2 ScaleToFitKeepingAspectRatio(Vector2 srcDims, Vector2 refDims)
        {
            Vector2 temp = srcDims;

            // Check width and scale object keeping aspect ratio
            if (temp.x > refDims.x || temp.x < refDims.x)
            {
                // Set width to fit the new rectangle
                temp.x = refDims.x;

                // Scale height proportionally
                temp.y = (temp.x * srcDims.y) / srcDims.x;
            } // if

            // Then check height
            if (temp.y > refDims.y)
            {
                // If the height keeps being higher, 
                // initialize again the dimensions
                if (temp != srcDims)
                {
                    temp = srcDims;
                } // if

                // Set height to fit the reference one
                temp.y = refDims.y;

                // Scale width proportionally 
                temp.x = (temp.y * srcDims.x) / srcDims.y;
            } // if

            return temp;
        } // ScaleToFitKeepingAspectRatio

        /// <summary>
        /// 
        /// Resize object's scale.
        /// 
        /// </summary>
        /// <param name="origUnits"> (Vector3) Original Units. </param>
        /// <param name="currUnits"> (Vector3) Current Units. </param>
        /// <param name="scale"> (Vector3) Scale. </param>
        /// <returns> (Vector3) New scale. </returns>
        public Vector3 resizeObjectScale(Vector3 origUnits, Vector3 currUnits, Vector3 scale)
        {
            Vector3 scalated = new Vector3();

            scalated.x = (currUnits.x * scale.x) / origUnits.x;
            scalated.y = (currUnits.y * scale.y) / origUnits.y;

            return scalated;
        } // resizeObjectScale

        /// <summary>
        /// 
        /// Resize the scale of an object keeping it's aspect ratio. 
        /// 
        /// </summary>
        /// <param name="origUnits"> (Vector3) Original Units. </param>
        /// <param name="currUnits"> (Vector3) Current Units. </param>
        /// <param name="scale"> (Vector3) Scale. </param>
        /// <returns> (Vector3) Scale resized. </returns>
        public Vector3 resizeObjectScaleKeepingAspectRatio(Vector3 origUnits, Vector3 currUnits, Vector3 scale)
        {
            // New scale to apply on the object
            Vector3 scalated = new Vector3();

            // Check width of the object
            if (origUnits.x > currUnits.x)
            {
                // Calculate new scale
                scalated.x = scalated.y = (currUnits.x * scale.x) / origUnits.x;
            } // if

            // Check height of the object
            if (origUnits.y > currUnits.y)
            {
                // If new scale has been calculated
                if (scalated.x != 0 && scalated.y != 0)
                {
                    // Reboot scale
                    scalated.x = scalated.y = 0;
                } // if 

                // Calculate new scale
                scalated.y = scalated.x = (currUnits.y * scale.y) / origUnits.y;
            } // if

            return scalated;
        } // resizeObjectScaleKeepingAspectRatio

        /// <summary>
        /// 
        /// Resize some value using the X coordinate of the 
        /// current resolution and the reference resolution.
        /// 
        /// </summary>
        /// <param name="x"> (float) Value to resize. </param>
        /// <returns> (float) Value resized. </returns>
        public float ResizeX(float x)
        {
            return (x * _currRes.x) / _refRes.x;
        } // ResizeX

        /// <summary>
        /// 
        /// Resize some value using the Y coordinate of the 
        /// current resolution and the reference resolution.
        /// 
        /// </summary>
        /// <param name="y"> (float) Value to resize. </param>
        /// <returns> (float) Value resized. </returns>
        public float ResizeY(float y)
        {
            return (y * _currRes.y) / _refRes.y;
        } // ResizeY

        /// <summary>
        /// 
        /// Get the value to convert from pixels to Unity Units and 
        /// viceversa.
        /// 
        /// </summary>
        /// <returns> (float) Conversion value. </returns>
        public float UnityUds()
        {
            return _unityUds;
        } // UnityUds

        /// <summary>
        /// 
        /// Get the value of the current game resolution. 
        /// 
        /// </summary>
        /// <returns> (Vector3) Current resolution. </returns>
        public Vector2 CurrentResolution()
        {
            return _currRes;
        } // CurrentResolution
    } // Scaler
} // namespace
