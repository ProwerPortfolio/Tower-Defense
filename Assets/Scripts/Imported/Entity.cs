using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ������� ����� ���� ������������� ������� �������� �� �����.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// �������� ������� ��� ������������.
        /// </summary>
        [SerializeField] private string nickname;
        public string Nickname => nickname;
    }

}