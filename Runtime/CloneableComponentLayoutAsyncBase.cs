using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

// ReSharper disable NotNullOrRequiredMemberIsNotInitialized

namespace Kogane
{
    /// <summary>
    /// コンポーネントを Instantiate で複製して管理するコンポーネント（非同期版）
    /// </summary>
    public abstract class CloneableComponentLayoutAsyncBase<TComponent, TData> :
        CloneableComponentLayoutCoreBase<TComponent, TData>
        where TComponent : Component, ICloneableComponentAsync<TData>
    {
        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// コンポーネントの表示を設定します
        /// 指定されたデータの数よりもコンポーネントが多い場合はコンポーネントを自動で削除します
        /// 指定されたデータの数よりもコンポーネントが少ない場合はコンポオーネントを自動で作成します
        /// </summary>
        public async UniTask SetupAsync( IReadOnlyList<TData> dataList )
        {
            SetupCore( dataList );

            var taskArray = new UniTask[ dataList.Count ];

            for ( var i = 0; i < dataList.Count; i++ )
            {
                var data      = dataList[ i ];
                var component = Elements[ i ];

                taskArray[ i ] = component.SetupAsync( data );
            }

            await UniTask.WhenAll( taskArray );
        }

        /// <summary>
        /// コンポーネントの表示を設定します
        /// 指定されたデータの数よりもコンポーネントが多い場合はコンポーネントを自動で削除します
        /// 指定されたデータの数よりもコンポーネントが少ない場合はコンポオーネントを自動で作成します
        /// </summary>
        public void Setup( IReadOnlyList<TData> dataList )
        {
            SetupAsync( dataList ).Forget();
        }
    }
}