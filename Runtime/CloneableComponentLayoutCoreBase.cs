using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

// ReSharper disable NotNullOrRequiredMemberIsNotInitialized

namespace Kogane
{
    /// <summary>
    /// コンポーネントを Instantiate で複製して管理するコンポーネントの
    /// コアな処理を管理する抽象クラス
    /// </summary>
    public abstract class CloneableComponentLayoutCoreBase<TComponent, TData> : MonoBehaviour
        where TComponent : Component
    {
        //================================================================================
        // 変数(SerializeField)
        //================================================================================
        [SerializeField][NotNull] private TComponent m_source; // 複製元のコンポーネント
        [SerializeField][NotNull] private Transform  m_parent; // 複製したコンポーネントの親

        //================================================================================
        // 変数(readonly)
        //================================================================================
        private List<TComponent> m_componentList; // 複製したコンポーネントを管理するリスト

        //================================================================================
        // プロパティ
        //================================================================================
        /// <summary>
        /// 複製したコンポーネントのリストを返します
        /// </summary>
        protected IReadOnlyList<TComponent> Elements => m_componentList;

        /// <summary>
        /// 複製したコンポーネントの数を返します
        /// </summary>
        protected int Count => m_componentList.Count;

        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// アタッチされた時に呼び出されます
        /// </summary>
        protected void Reset()
        {
            m_parent = transform;
        }

        /// <summary>
        /// コンポーネントの表示を設定します
        /// 指定されたデータの数よりもコンポーネントが多い場合はコンポーネントを自動で削除します
        /// 指定されたデータの数よりもコンポーネントが少ない場合はコンポオーネントを自動で作成します
        /// </summary>
        private protected void SetupCore( IReadOnlyList<TData> dataList )
        {
            m_componentList ??= new( dataList.Count );

            if ( m_source.gameObject.activeSelf )
            {
                m_source.gameObject.SetActive( false );
            }

            while ( dataList.Count < m_componentList.Count )
            {
                var component = m_componentList[ ^1 ];
                m_componentList.Remove( component );
                Destroy( component.gameObject );
            }

            while ( m_componentList.Count < dataList.Count )
            {
                var component = Instantiate
                (
                    original: m_source,
                    parent: m_parent
                );

                component.gameObject.SetActive( true );

                m_componentList.Add( component );
            }
        }
    }
}