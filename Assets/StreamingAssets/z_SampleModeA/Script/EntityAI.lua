Include = {"Math", "Vector", "Time"}

-- gameEntity
ActiveTime = 0;

function Reset ()
    ActiveTime = 0;

    print("Reset Behavior");
end

function Update ()

    -- ActiveTime = ActiveTime + Time.delta;
    local rigidbody = Entity.GetComponentByID("Rigidbody"); 
    -- rigidbody.AddForce(Vector.up * 500);
    rigidbody.velocity = Vector.up * 10;
    -- print(health);
end
