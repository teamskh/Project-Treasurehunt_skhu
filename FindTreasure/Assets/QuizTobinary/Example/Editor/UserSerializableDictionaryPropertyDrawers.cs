using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DataInfo;

/*[CustomPropertyDrawer(typeof(StringStringDictionary))]
[CustomPropertyDrawer(typeof(ObjectColorDictionary))]
[CustomPropertyDrawer(typeof(StringColorArrayDictionary))]*/
[CustomPropertyDrawer(typeof(QuizInfoDictionary))]
[CustomPropertyDrawer(typeof(ContestDictionary))]
public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}

/*[CustomPropertyDrawer(typeof(ColorArrayStorage))]
public class AnySerializableDictionaryStoragePropertyDrawer: SerializableDictionaryStoragePropertyDrawer {}
*/