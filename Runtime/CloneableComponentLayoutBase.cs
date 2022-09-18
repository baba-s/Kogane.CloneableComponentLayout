using System.Collections.Generic;
using UnityEngine;

// ReSharper disable NotNullOrRequiredMemberIsNotInitialized

namespace Kogane
{
    /// <summary>
    /// コンポーネントを Instantiate で複製して管理するコンポーネント
    /// </summary>
    public abstract class CloneableComponentLayoutBase<TComponent, TData> :
        CloneableComponentLayoutCoreBase<TComponent, TData>
        where TComponent : Component, ICloneableComponent<TData>
    {
        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// コンポーネントの表示を設定します
        /// 指定されたデータの数よりもコンポーネントが多い場合はコンポーネントを自動で削除します
        /// 指定されたデータの数よりもコンポーネントが少ない場合はコンポオーネントを自動で作成します
        /// </summary>
        public void Setup( IReadOnlyList<TData> dataList )
        {
            SetupCore( dataList );

            for ( var i = 0; i < dataList.Count; i++ )
            {
                var data      = dataList[ i ];
                var component = Elements[ i ];

                component.Setup( data );
            }
        }
    }
}