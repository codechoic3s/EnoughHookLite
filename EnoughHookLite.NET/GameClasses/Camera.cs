﻿using EnoughHookLite.Modules;
using EnoughHookLite.Pointing;
using EnoughHookLite.Pointing.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.GameClasses
{
    public sealed class Camera
    {
        public float[] ViewMatrix { get; private set; }
        public bool IsWorking { get; private set; }

        public int ViewMatrixSize { get; private set; }

        private SubAPI SubAPI;

        public Camera(SubAPI api)
        {
            SubAPI = api;
            ViewMatrixSize = 16;
            ViewMatrix = new float[ViewMatrixSize];
        }

        [Signature(SignaturesConsts.dwViewMatrix)]
        private PointerCached pViewMatrix;

        internal async void ViewMatrixFetcher()
        {
            IsWorking = true;
            while (IsWorking)
            {
                uint vmbase = SubAPI.Client.NativeModule.BaseAdr + pViewMatrix.Pointer;
                ViewMatrix = SubAPI.Process.RemoteMemory.ReadFloatArray(vmbase, 16);
                /*
                for (int i = 0; i < ViewMatrixSize; i++)
                {
                    ViewMatrix[i] = SubAPI.Client.NativeModule.Process.RemoteMemory.ReadFloat(vmbase + (i * 4));
                }
                */
                await Task.Delay(5);
            }
        }

        public Vector2 WorldToScreen(Vector3 target)
        {
            //Vector3 to;
            float w; // = 0.0f;
            float[] viewmatrix = ViewMatrix;

            w = viewmatrix[12] * target.X + viewmatrix[13] * target.Y + viewmatrix[14] * target.Z + viewmatrix[15];

            // behind us
            if (w < 0.01f)
                return new Vector2(0, 0);

            Vector2 _worldToScreenPos;

            _worldToScreenPos.X = viewmatrix[0] * target.X + viewmatrix[1] * target.Y + viewmatrix[2] * target.Z + viewmatrix[3];
            _worldToScreenPos.Y = viewmatrix[4] * target.X + viewmatrix[5] * target.Y + viewmatrix[6] * target.Z + viewmatrix[7];

            _worldToScreenPos.X *= (1.0f / w);
            _worldToScreenPos.Y *= (1.0f / w);

            float width = SubAPI.Process.Size.X;
            float height = SubAPI.Process.Size.Y;

            float x = SubAPI.Process.MidSize.X;
            float y = SubAPI.Process.MidSize.Y;

            x += 0.5f * _worldToScreenPos.X * width + 0.5f;
            y -= 0.5f * _worldToScreenPos.Y * height + 0.5f;

            _worldToScreenPos.X = x;
            _worldToScreenPos.Y = y;

            return _worldToScreenPos;
        }
    }
}
