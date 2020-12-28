using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForbiddenReef {
    [CreateAssetMenu (menuName = "Items/Weapon")]
    public class Weapon : Item {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header ("One Handed Attack Animations")]
        public string Light_attack_1;
        public string Light_attack_2;
        public string Heavy_attack_1;
    }
}