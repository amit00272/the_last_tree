using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour,IStoreListener {

	public static  IAPManager Instance = null;
	static IStoreController m_storeController;
	static IExtensionProvider m_storeExtentionProvider;
	
	public const string productidnoads = "";
	public const string PRODUCT_SEED_10 = "com.thelasttree.seed10";
	public const string PRODUCT_SEED_20 = "com.thelasttree.seed20";
	public const string PRODUCT_SEED_50 = "com.thelasttree.seed50";
	public const string PRODUCT_SEED_100 = "com.thelasttree.seed100";
	



	void Awake ()
	{

		
		if (Instance == null) {

			Instance = this;

			DontDestroyOnLoad (gameObject);
		} else {

			Destroy(gameObject);
		} 

	}

	void Start (){


		if(!isInitialized())
			InitializePurchasing();
	}

	public void InitializePurchasing ()
	{

		if (isInitialized ()) {

			return;

		}

		StandardPurchasingModule module = StandardPurchasingModule.Instance();

		ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);

		//builder.AddProduct(productidnoads,ProductType.NonConsumable);
		builder.AddProduct(PRODUCT_SEED_10,ProductType.Consumable);
		builder.AddProduct(PRODUCT_SEED_20,ProductType.Consumable);
		builder.AddProduct(PRODUCT_SEED_50,ProductType.Consumable);
		builder.AddProduct(PRODUCT_SEED_100,ProductType.Consumable);
		UnityPurchasing.Initialize(this, builder);
	}

	public void OnInitialized(IStoreController controller,IExtensionProvider extensions){

		m_storeController = controller;
		m_storeExtentionProvider = extensions;
	}


	public void OnInitializeFailed(InitializationFailureReason error){

	  Debug.LogWarning("IAP on initialization failed");

	}

	public void OnPurchaseFailed (Product i,PurchaseFailureReason p){

	  Debug.Log("Purchase failed for pruduct id" + i.definition.storeSpecificId);
	}

	public bool isInitialized (){

		return m_storeController != null &&
			   m_storeExtentionProvider !=null;
				
	}


	public PurchaseProcessingResult ProcessPurchase (PurchaseEventArgs e)
	{

		string productId = e.purchasedProduct.definition.id;

		switch (productId)
		{
			case PRODUCT_SEED_10 : PrefsManager.instance.IncreaseSeedVal(10); break;
			case PRODUCT_SEED_20 : PrefsManager.instance.IncreaseSeedVal(20); break;
			case PRODUCT_SEED_50 : PrefsManager.instance.IncreaseSeedVal(50); break;
			case PRODUCT_SEED_100 : PrefsManager.instance.IncreaseSeedVal(100); break;
			default: break;
		}
			
	return PurchaseProcessingResult.Complete;

	}


	public void BuyProductID (string productId)
	{


		if (isInitialized ()) {

			Product product = m_storeController.products.WithID (productId);
			if (product != null && product.availableToPurchase) {

				m_storeController.InitiatePurchase (product);
				Debug.Log ("Purchasing start on" + productId);

			} else {

				Debug.Log ( productId + "Not Found");
			}
			
		}else {

			Debug.LogWarning("Iap manager product errpr" + productId);

		}

	}


	public void setNoAds(){

	

	}


	//Puchase methods called from outside
	public void BuyNoAds ()
	{

		BuyProductID(productidnoads);
	}




	public void BuyProduct (int num){

		BuyProductID(num == 1 ? PRODUCT_SEED_10 :
					 num == 2 ? PRODUCT_SEED_20 :
					 num == 3 ? PRODUCT_SEED_50 :  PRODUCT_SEED_100 );
	}
}
